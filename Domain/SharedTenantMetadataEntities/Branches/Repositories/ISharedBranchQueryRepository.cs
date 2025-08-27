namespace Domain.SharedTenantMetadataEntities.Branches.Repositories;

public interface ISharedBranchQueryRepository
{
    Task<SharedBranch?> GetByIdAsync(SharedBranchId? id, CancellationToken cancellationToken = default);

    Task<List<SharedBranch>> GetAllAsync(CancellationToken cancellationToken = default);
}
