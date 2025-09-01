using Domain.CatalogDb.Branches.ObjectValues;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Branches.Repositories;

namespace Application.Features.Brnches.DeleteBranch;

public sealed class DeleteBranchCommandHandler(
    ISharedBranchQueryRepository sharedBranchQueryRepository,
    ISharedBranchCommandRepository sharedBranchCommandRepository,
    IBranchQueryRepository branchQueryRepository,
    IBranchCommandRepository branchCommandRepository,
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    ITenantService tenantService,
    IConnectionStringProtector connectionStringProtector,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<DeleteBranchCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var sharedBranch = await sharedBranchQueryRepository.GetByIdAsync(new SharedBranchId(new Guid(request.Id)), cancellationToken);

        if (sharedBranch is not null)
        {
            var branch = await branchQueryRepository.GetByIdAsync(new BranchId(new Guid(request.Id)), cancellationToken);

            if (branch is not null)
            {
                branchCommandRepository.DeleteAsync(branch, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                await sharedBranchCommandRepository.DeleteAsync(sharedBranch, cancellationToken);

                return Success("Branch Deleted Successfully.");
            }
        }
        return InternalServerError<string>("Server error while deleting the branch.");
    }
}
