using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users.Repositories.Users;

public interface IUserCommandRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(UserId userId);

}
