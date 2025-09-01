namespace Domain.CatalogDb.Repositories;

public interface ISharedTenantCommandRepository
{
    Task AddAsync(SharedTenant tenant, CancellationToken cancellationToken = default);

    Task UpdateAsync(SharedTenant tenant, CancellationToken cancellationToken = default);

    Task DeleteAsync(SharedTenant tenant, CancellationToken cancellationToken = default);
}
