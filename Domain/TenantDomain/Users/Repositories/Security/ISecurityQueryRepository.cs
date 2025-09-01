using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users.Repositories.Securities;

public interface ISecurityQueryRepository
{
    Task<Security?> GetByIdAsync(UserId userId);
    Task<List<Security>> GetAllAsync();
}
