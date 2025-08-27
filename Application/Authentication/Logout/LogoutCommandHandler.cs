using Application.Services.UnitOfWork;
using Domain.SharedTenantMetadataEntities.Tenants.ObjectValues;
using System.Security.Claims;

namespace Application.Authentication.Logout;

public sealed class LogoutCommandHandler(
    IHttpContextAccessor httpContextAccessor,
    ISharedUserTokenQueryRepository sharedUserTokenQueryRepository,
    ISharedUserTokenCommandRepository sharedUserTokenCommandRepository,
    IUserTokenQueryRepository userTokenQueryRepository,
    IUserTokenCommandRepository userTokenCommandRepository,
    IConnectionStringProtector connectionStringProtector,
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    ITenantService tenantService,
    IConfiguration configuration,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<LogoutCommand, Response<string>>
{
    public async Task<Response<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var role = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

        if (role == "Admin")
        {
            var connectionString = configuration.GetSection("TenantConnection").Value;
            tenantService.SetConnectionString(connectionString!);
        }
        else
        {
            var tenantId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "TenantId").Value;

            var tenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(new Guid(tenantId)), cancellationToken);

            var connectionString = connectionStringProtector.Decrypt(tenant!.ConnectionString);

            tenantService.SetConnectionString(connectionString);

        }
        var userTokenId = httpContextAccessor.HttpContext.Request.Cookies["user-token-id"];


        var sharedUserToken = await sharedUserTokenQueryRepository.GetByIdWithBranchAsync(new SharedUserTokenId(new Guid(userTokenId)), cancellationToken);

        if (sharedUserToken is not null)
        {
            var userToken = await userTokenQueryRepository.GetByIdAsync(new UserTokenId(new Guid(userTokenId)), cancellationToken);

            if (userToken is not null)
            {
                userToken.MarkAsInactive();

                userTokenCommandRepository.UpdateAsync(userToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                sharedUserToken.MarkAsInactive();

                await sharedUserTokenCommandRepository.UpdateAsync(sharedUserToken);

                var cookies = httpContextAccessor.HttpContext.Request.Cookies;

                var cookieKeys = cookies.Select(c => c.Key).ToList();

                foreach (var key in cookieKeys)
                {
                    httpContextAccessor.HttpContext.Response.Cookies.Delete(key, new CookieOptions
                    {
                        Path = "/",
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                }

                return Success("User logged out successfully.");
            }

        }

        return InternalServerError<string>("Failed to logout.");

    }
}
