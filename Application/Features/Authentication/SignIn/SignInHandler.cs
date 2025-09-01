namespace Application.Features.Authentication.SignIn;

public sealed class SignInHandler(
    IAuthenticationService authenticationService,
    IUserQueryRepository userQueryRepository,
    IUserTokenCommandRepository userTokenCommandRepository,
    IPasswordHashingService passwordHashingService,
    ITenantService tenantService,
    IServiceProvider serviceProvider,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContextAccessor)

    : ResponseHandler,
    IRequestHandler<SignInCommand, Response<UserTokenDto>>

{
    public async Task<Response<UserTokenDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await tenantService.SetConnectionStringForSignIn(request.Email, serviceProvider);

        }
        catch
        {
            return BadRequest<UserTokenDto>("Invalid email or password.");
        }

        var user = await userQueryRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return BadRequest<UserTokenDto>("Invalid email or password.");

        if (!passwordHashingService.VerifyPasswordBCrypt(request.Password, user.HashedPassword))
            return BadRequest<UserTokenDto>("Invalid email or password.");

        var refreshTokenExpiredDate = DateTime.UtcNow.AddMonths(3);

        var (userTokenDto, newAccessToken, newRefreshToken) = await authenticationService.CreateToken(user, refreshTokenExpiredDate, null);

        var userToken = UserToken.Create(
            new UserTokenId(Guid.NewGuid()),
            newAccessToken,
            userTokenDto.AccessTokenExpiredDate,
            newRefreshToken,
            refreshTokenExpiredDate,
            new UserId(new Guid(userTokenDto.UserId))
            );

        await userTokenCommandRepository.AddAsync(userToken);

        var branchId = tenantService.GetBranchId();

        userToken.RaiseNewTokenCreatedEvent(branchId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        SetAuthCookies(userToken);

        return Success(userTokenDto);
    }

    private void SetAuthCookies(UserToken userToken)
    {
        httpContextAccessor.HttpContext.Response.Cookies
            .Append("access-token", userToken.AccessToken,
                                     GetCookieOptions(userToken.AccessTokenExpiredDate));

        httpContextAccessor.HttpContext.Response.Cookies
            .Append("refresh-token", userToken.RefreshToken,
                                        GetCookieOptions(userToken.RefreshTokenExpiredDate));

        httpContextAccessor.HttpContext.Response.Cookies
            .Append("user-token-id", userToken.Id.Value.ToString(),
                                        GetCookieOptions(userToken.RefreshTokenExpiredDate));
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