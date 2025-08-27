using Application.Services.UnitOfWork;
using Domain.Branches.Repositories;
using Domain.SharedTenantMetadataEntities.Repositories;
using Domain.SharedTenantMetadataEntities.Tenants.ObjectValues;
using Domain.Tenants.ObjectValues;

namespace Application.Admin.DeleteTenantWithSharedDb;

public class DeleteTenantWithSharedDbCommandHandler(
    ISharedTenantQueryRepository sharedTenantQueryRepository,
    ISharedTenantCommandRepository sharedTenantCommandRepository,
    ITenantCommandRepository tenantCommandRepository,
    ITenantQueryRepository tenantQueryRepository,
    IBranchQueryRepository branchQueryRepository,
    IBranchCommandRepository branchCommandRepository,
    IUnitOfWork unitOfWork)
    : ResponseHandler,
    IRequestHandler<DeleteTenantWithSharedDbCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteTenantWithSharedDbCommand request, CancellationToken cancellationToken)
    {
        var exsistedTenant = await sharedTenantQueryRepository.GetByIdAsync(new SharedTenantId(new Guid(request.TenantId)), cancellationToken);

        if (exsistedTenant is null)
            return BadRequest<string>("There is no tenant with that name");


        await sharedTenantCommandRepository.DeleteAsync(exsistedTenant, cancellationToken);

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var tenant = await tenantQueryRepository.GetByIdAsync(new TenantId(new Guid(request.TenantId)), cancellationToken);

            if (tenant is null)
                return BadRequest<string>("There is no tenant with that name");

            var branches = await branchQueryRepository.GetByITenantIdAsync(tenant.Id, cancellationToken);

            branchCommandRepository.DeleteListAsync(branches, cancellationToken);

            tenantCommandRepository.DeleteAsync(tenant);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();

            return InternalServerError<string>(ex.Message);
        }

        return Success("Tenant Deleted Successfully");

    }
}
