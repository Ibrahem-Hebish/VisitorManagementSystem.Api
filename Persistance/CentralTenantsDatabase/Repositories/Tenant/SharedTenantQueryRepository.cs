
namespace Persistence.CentralTenantsDatabase.Repositories.Tenant;

public class SharedTenantQueryRepository(SharedDbContext sharedDbContext) : ISharedTenantQueryRepository
{
    public async Task<List<SharedTenant>> GetAllAsync(CancellationToken cancellationToken = default)
                                                   => await sharedDbContext.SharedTenants
                                                                            .ToListAsync(cancellationToken);

    public async Task<SharedTenant?> GetByIdAsync(SharedTenantId tenantId, CancellationToken cancellationToken = default)
                                                   => await sharedDbContext.SharedTenants
                                                                 .FirstOrDefaultAsync(t => t.Id == tenantId, cancellationToken);

    public async Task<SharedTenant?> GetByNameAsync(string tenantName, CancellationToken cancellationToken = default)
                                                   => await sharedDbContext.SharedTenants
                                                                    .FirstOrDefaultAsync(t => t.Name == tenantName, cancellationToken);

    public async Task<SharedTenant?> GetByIdWithDetailsAsync(SharedTenantId tenantId, CancellationToken cancellationToken = default)
                                                   => await sharedDbContext.SharedTenants
                                                                .Include(t => t.Branches)
                                                                .FirstOrDefaultAsync(t => t.Id == tenantId, cancellationToken);

}
