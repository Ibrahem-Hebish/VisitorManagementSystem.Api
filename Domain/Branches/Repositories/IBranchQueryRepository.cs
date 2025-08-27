namespace Domain.Branches.Repositories;

public interface IBranchQueryRepository
{
    Task<Branch?> GetByIdAsync(BranchId id, CancellationToken cancellationToken = default);
    Task<Branch?> GetDetailsAsync(BranchId id, CancellationToken cancellationToken = default);
    Task<List<Branch>> GetByITenantIdAsync(TenantId id, CancellationToken cancellationToken = default);

    Task<List<Branch>> GetAllAsync(CancellationToken cancellationToken = default);
}
