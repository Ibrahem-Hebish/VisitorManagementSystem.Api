using Domain.TenantDomain.Roles;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users.Repositories.Users;

public interface IUserQueryRepository
{
    Task<User?> GetByIdAsync(UserId userId);
    Task<User?> GetByEmailAsync(string email);
    Task<List<User>> GetAllAsync();
    Task<Role?> GetUserRole(UserId userId);
}
