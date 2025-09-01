namespace Persistence.CentralTenantsDatabase.Repositories.Tenant;

public class SharedTenantCommandRepository(SharedDbContext sharedDbContext) : ISharedTenantCommandRepository
{
    public async Task AddAsync(SharedTenant tenant, CancellationToken cancellationToken = default)
    {
        await sharedDbContext.SharedTenants.AddAsync(tenant, cancellationToken);
        await sharedDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SharedTenant tenant, CancellationToken cancellationToken = default)
    {
        sharedDbContext.SharedTenants.Update(tenant);
        await sharedDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SharedTenant tenant, CancellationToken cancellationToken = default)
    {
        sharedDbContext.SharedTenants.Remove(tenant);
        await sharedDbContext.SaveChangesAsync(cancellationToken);
    }
}
