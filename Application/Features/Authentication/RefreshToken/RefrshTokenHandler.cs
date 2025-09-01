using Application.Dtos.UserToken;
using Application.Services.Authentication;
using Domain.TenantDomain.Tokens;
using Domain.TenantDomain.Tokens.Repositories;
using Domain.TenantDomain.Tokens.ValueObjects;
using Domain.TenantDomain.Users.Repositories.Users;

namespace Application.Features.Authentication.RefreshToken;

public class RefrshTokenHandler(
    IAuthenticationService authenticationService,
    ISharedUserTokenCommandRepository sharedUserTokenCommandRepository,
    IUserTokenCommandRepository userTokenCommandRepository,
    IUserQueryRepository userQueryRepository,
    IUserTokenQueryRepository userTokenQueryRepository,
    ITenantService tenantService,
    IServiceProvider serviceProvider,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor
    )

    : ResponseHandler,
    IRequestHandler<RefreshTokenCommand, Response<UserTokenDto>>
{

    public async Task<Response<UserTokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = httpContextAccessor.HttpContext.Request.Cookies["refresh-token"];

        if (string.IsNullOrWhiteSpace(refreshToken))
            return BadRequest<UserTokenDto>("Refresh token is missing in cookies.");

        var userTokenId = httpContextAccessor.HttpContext.Request.Cookies["user-token-id"];

        if (string.IsNullOrWhiteSpace(userTokenId))
            return BadRequest<UserTokenDto>("User token id is missing in cookies.");

        var sharedToken = await tenantService.SetConnectionStringRefreshToken(userTokenId, serviceProvider);


        var userToken = await userTokenQueryRepository.GetByIdAsync(
                                    new UserTokenId(new Guid(userTokenId)), cancellationToken);

        if (userToken is null)
            return BadRequest<UserTokenDto>("User token not found.");

        if (refreshToken != userToken.RefreshToken)
            return BadRequest<UserTokenDto>("Invalid Token");

        if (userToken.AccessTokenExpiredDate > DateTime.UtcNow)
            return BadRequest<UserTokenDto>("Access token is not expired yet");

        if (userToken.RefreshTokenExpiredDate < DateTime.UtcNow)
        {
            userToken.MarkAsInactive();

            sharedToken.MarkAsInactive();

            userTokenCommandRepository.UpdateAsync(userToken);

            try
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);

                await sharedUserTokenCommandRepository.UpdateAsync(sharedToken);
            }
            catch
            {
                throw new Exception($"Error while expiration of an old token with id {userToken.Id.Value}");
            }

            return BadRequest<UserTokenDto>("Refresh token is expired");
        }

        var user = await userQueryRepository.GetByIdAsync(userToken.UserId);

        if (user is null)
            return BadRequest<UserTokenDto>("User not found.");

        var refreshTokenExpireDate = userToken.RefreshTokenExpiredDate;

        var (userTokenDto, newAccessToken, newRefreshToken) = await authenticationService.CreateToken(user, userToken.RefreshTokenExpiredDate, refreshToken);

        userToken.Update(newAccessToken, newRefreshToken, refreshTokenExpireDate, userTokenDto.AccessTokenExpiredDate);

        userToken.RaiseTokenUpdatedEvent(tenantService.GetTenantId());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        SetAuthCookies(userToken);

        await Console.Out.WriteLineAsync(userToken.RefreshTokenExpiredDate.ToString());

        return Success(userTokenDto);
    }

    private void SetAuthCookies(UserToken userToken)
    {

        httpContextAccessor.HttpContext.Response.Cookies
            .Append("access-token", userToken.AccessToken,
                                     GetCookieOptions(userToken.AccessTokenExpiredDate));


    }
    private static CookieOptions GetCookieOptions(DateTime expirationDate)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = expirationDate
        };
    }
}
