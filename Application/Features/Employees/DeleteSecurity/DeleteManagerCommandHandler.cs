using Application.Services.UnitOfWork;
using Domain.TenantDomain.Users.Repositories.Securities;

namespace Application.Features.Employees.DeleteSecurity;

public sealed class DeleteManagerCommandHandler(
    ISecurityQueryRepository securityQueryRepository,
    ISecurityCommandRepository securityCommandRepository,
    IUnitOfWork unitOfWork
    )
    : ResponseHandler,

    IRequestHandler<DeleteSecurityCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteSecurityCommand request, CancellationToken cancellationToken)
    {
        var security = await securityQueryRepository.GetByIdAsync(new UserId(new Guid(request.Id)));

        if (security is null)
            return NotFound<string>("There is no security with that id.");

        securityCommandRepository.DeleteAsync(security);

        security.RaiseEmployeeDeletedDomainEvent(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Deleted<string>("Security Deleted Successfully.");
    }
}