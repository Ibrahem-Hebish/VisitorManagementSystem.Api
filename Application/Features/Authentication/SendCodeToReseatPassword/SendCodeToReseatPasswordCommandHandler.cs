
using Domain.TenantDomain.Users.Repositories.Users;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;

namespace Application.Features.Authentication.SendCodeToReseatPassword;

public sealed class SendCodeToReseatPasswordCommandHandler(
    IUserQueryRepository userQueryRepository,
    IServiceProvider serviceProvider,
    IMemoryCache cache,
    ITenantService tenantService)

    : ResponseHandler,
    IRequestHandler<SendCodeToReseatPasswordCommand, Response<string>>
{
    public async Task<Response<string>> Handle(SendCodeToReseatPasswordCommand request, CancellationToken cancellationToken)
    {

        await tenantService.SetConnectionStringForResetPassword(request.Email, serviceProvider);

        var user = await userQueryRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return NotFound<string>();

        var code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        cache.Set($"reset-password-{request.Email}", code, TimeSpan.FromMinutes(5));

        return Success(code);

    }
}
