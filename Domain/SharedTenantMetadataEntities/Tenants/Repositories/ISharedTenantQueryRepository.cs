namespace Domain.Tenants.Repositories;

public interface ISharedTenantQueryRepository
{
    Task<SharedTenant?> GetByIdAsync(SharedTenantId tenantId, CancellationToken cancellationToken = default);

    Task<SharedTenant?> GetByIdWithDetailsAsync(SharedTenantId tenantId, CancellationToken cancellationToken = default);

    Task<SharedTenant?> GetByNameAsync(string tenantName, CancellationToken cancellationToken = default);

    Task<List<SharedTenant>> GetAllAsync(CancellationToken cancellationToken = default);
}
