using Domain.Roles;

namespace Domain.Users.Repositories.Users;

public interface IUserQueryRepository
{
    Task<User?> GetByIdAsync(UserId userId);
    Task<User?> GetByEmailAsync(string email);
    Task<List<User>> GetAllAsync();
    Task<Role?> GetUserRole(UserId userId);
}
