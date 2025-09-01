using Domain.TenantDomain.Tenants;
using Domain.TenantDomain.Tenants.ObjectValues;

namespace Domain.TenantDomain.Tenants.Repositories;

public interface ITenantQueryRepository
{
    Task<Tenant?> GetByIdAsync(TenantId tenantId, CancellationToken cancellationToken = default);

    Task<Tenant?> GetByNameAsync(string tenantName, CancellationToken cancellationToken = default);

    Task<List<Tenant>> GetAllAsync(CancellationToken cancellationToken = default);
}
