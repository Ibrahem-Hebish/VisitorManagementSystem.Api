using Domain.TenantDomain.Tenants;
using Domain.TenantDomain.Tenants.Repositories;

namespace Persistence.TenantDb.Repositories.Tenants;

public class TenantCommandRepository(TenantDbContext dbContext) : ITenantCommandRepository
{
    public async Task AddAsync(Tenant tenant, CancellationToken cancellationToken = default)
                                                       => await dbContext.Tenants.AddAsync(tenant, cancellationToken);


    public void DeleteAsync(Tenant tenant) => dbContext.Tenants.Remove(tenant);
    public void UpdateAsync(Tenant tenant) => dbContext.Tenants.Update(tenant);
}
