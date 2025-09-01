using Domain.TenantDomain.Tenants;
using Domain.TenantDomain.Tenants.ObjectValues;
using Domain.TenantDomain.Tenants.Repositories;

namespace Persistence.TenantDb.Repositories.Tenants;

public class TenantQueryRepository(TenantDbContext tenantDbContext) : ITenantQueryRepository
{
    private readonly TenantDbContext _tenantDbContext = tenantDbContext;


    public async Task<Tenant?> GetByIdAsync(TenantId tenantId, CancellationToken cancellationToken = default)
    {

        return await _tenantDbContext.Tenants
            .FindAsync(tenantId);
    }

    public async Task<Tenant?> GetWithBranchesAsync(TenantId tenantId, CancellationToken cancellationToken = default)
    {

        var tenant = await _tenantDbContext.Tenants
            .FindAsync(tenantId);

        if (tenant is not null)
        {
            await _tenantDbContext.Entry(tenant).Collection(x => x.Branches).LoadAsync(cancellationToken);
        }

        return tenant;
    }

    public async Task<Tenant?> GetByNameAsync(string tenantName, CancellationToken cancellationToken = default)
    {

        return await _tenantDbContext.Tenants
            .FirstOrDefaultAsync(x => x.Name == tenantName, cancellationToken: cancellationToken);
    }

    public async Task<List<Tenant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _tenantDbContext.Tenants
            .ToListAsync(cancellationToken: cancellationToken);
    }


}
