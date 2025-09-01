using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Tenants.ObjectValues;

namespace Domain.TenantDomain.Branches.Repositories;

public interface IBranchQueryRepository
{
    Task<Branch?> GetByIdAsync(BranchId id, CancellationToken cancellationToken = default);
    Task<Branch?> GetDetailsAsync(BranchId id, CancellationToken cancellationToken = default);
    Task<List<Branch>> GetByTenantIdAsync(TenantId id, CancellationToken cancellationToken = default);

    Task<List<Branch>> GetAllAsync(CancellationToken cancellationToken = default);
}
