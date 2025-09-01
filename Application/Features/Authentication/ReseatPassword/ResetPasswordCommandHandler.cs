
using Domain.TenantDomain.Users.Repositories.Users;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace Application.Features.Authentication.ReseatPassword;

public sealed class ResetPasswordCommandHandler(
    IUserQueryRepository userQueryRepository,
    IUnitOfWork unitOfWork,
    IServiceProvider serviceProvider,
    IPasswordHashingService passwordHashingService,
    ITenantService tenantService,
    IMemoryCache memoryCache
    )

    : ResponseHandler,
    IRequestHandler<ResetPasswordCommand, Response<string>>
{
    public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!memoryCache.TryGetValue($"reseat-password-{request.Email}", out var cachedCode) ||
                !string.Equals(request.Code, cachedCode?.ToString(), StringComparison.Ordinal))
            {
                return BadRequest<string>("Invalid code.");
            }

            await tenantService.SetConnectionStringForResetPassword(request.Email, serviceProvider);

            var user = await userQueryRepository.GetByEmailAsync(request.Email);

            if (user is null)
                return NotFound<string>();

            var password = passwordHashingService.HashPasswordBCrypt(request.NewPassword);

            user.ChangePassword(password);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            memoryCache.Remove($"reset-password-{request.Email}");

            return Success("Password is reseated successfully.");
        }
        catch (Exception ex)
        {
            Log.Error($"error while reaset password for user with email{request.Email}. error message {ex.Message}");

            return InternalServerError<string>();
        }

    }
}
