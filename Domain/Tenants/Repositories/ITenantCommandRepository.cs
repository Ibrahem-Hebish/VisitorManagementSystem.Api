namespace Domain.Tenants.Repositories;

public interface ITenantCommandRepository
{
    Task AddAsync(Tenant tenant, CancellationToken cancellationToken = default);

    void UpdateAsync(Tenant tenant);

    void DeleteAsync(Tenant tenant);
}
