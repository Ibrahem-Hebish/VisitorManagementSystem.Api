using Domain.TenantDomain.Branches.Repositories;
using Domain.TenantDomain.Users.Enums;
using Domain.TenantDomain.Users.Repositories.Employees;
using Serilog;

namespace Application.Features.Employees.DeleteBranchAdmin;

public sealed class DeleteBranchAdminCommandHandler(
    IEmployeeCommandRepository employeeCommandRepository,
    IEmployeeQueryRepository employeeQueryRepository,
    IBranchQueryRepository branchQueryRepository,
    IUnitOfWork unitOfWork
    )

    : ResponseHandler,
    IRequestHandler<DeleteBranchAdminCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteBranchAdminCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var branchAdmin = await employeeQueryRepository.GetByIdAsync(new UserId(new Guid(request.Id)));

            if (branchAdmin is null)
                return BadRequest<string>("There is no employee to delete with this id.");

            if (branchAdmin.Position != EmployeePosition.BranchAdmin)
                return UnAuthorize<string>();

            var branch = await branchQueryRepository.GetByIdAsync(branchAdmin.BranchId, cancellationToken);

            if (branch is null)
                return NotFound<string>("branch is not found.");

            branch.UnsetManager();

            employeeCommandRepository.Delete(branchAdmin);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Deleted<string>("Branch admin is deleted successfully.");
        }catch (Exception ex)
        {
            Log.Error($"Error while deleting branch admin. message: {ex.Message}");

            return InternalServerError<string>();
        }

    }
}
