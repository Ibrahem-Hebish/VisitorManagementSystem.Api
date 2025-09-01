using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users.Repositories.Managers;

public interface IManagerQueryRepository
{
    Task<Manager?> GetByIdAsync(UserId userId);
    Task<List<Manager>> GetAllAsync();
}
