using Serilog;

namespace Application.Features.Authentication.ChangePassword;

public sealed class ChangePasswordCommandHandler(
    IUserQueryRepository userQueryRepository,
    IPasswordHashingService passwordHashingService,
    IHttpContextAccessor httpContextAccessor,
    IServiceProvider serviceProvider,
    ITenantService tenantService,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<ChangePasswordCommand, Response<string>>
{
    public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        await tenantService.SetConnectionStringForChangePassword(serviceProvider, httpContextAccessor);

        var id = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        try
        {
            var user = await userQueryRepository.GetByIdAsync(new UserId(new Guid(id)));

            if (user is null)
                return NotFound<string>();

            if (!passwordHashingService.VerifyPasswordBCrypt(request.Password, user.HashedPassword))
                return BadRequest<string>("password doesn't match old password.");

            user.ChangePassword(passwordHashingService.HashPasswordBCrypt(request.NewPassword));

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Success<string>("Password Changed Successfully.");
        }
        catch (Exception ex)
        {
            Log.Error($"Error while changing password for user with id {id}.{ex.Message}");

            return InternalServerError<string>();
        }

    }
}
