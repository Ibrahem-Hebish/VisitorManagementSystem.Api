using Domain.TenantDomain.Permits;

namespace Domain.TenantDomain.Permits.Repositories;

public interface IPermitCommandRepository
{
    Task CreateAsync(Permit permit, CancellationToken cancellationToken = default);
    void Delete(Permit permit);
    Task DeleteDependenciesAsync(string permitId);
    void Update(Permit permit);
}
