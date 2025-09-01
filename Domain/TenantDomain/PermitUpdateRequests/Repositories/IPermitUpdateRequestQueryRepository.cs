using Domain.TenantDomain.PermitUpdateRequests.ObjectValues;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.PermitUpdateRequests.Repositories;

public interface IPermitUpdateRequestQueryRepository
{
    Task<PermitUpdateRequest?> GetByIdAsync(PermitUpdateRequestId id);
    Task<List<PermitUpdateRequest>> GetAllAsync();
    Task<List<PermitUpdateRequest>> GetLatestAsync();
    Task<List<PermitUpdateRequest>> GetByRequesterId(UserId userId);
}
