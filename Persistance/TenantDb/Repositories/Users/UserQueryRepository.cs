using Domain.Users.Repositories.Users;

namespace Persistence.TenantDb.Repositories.Users;

public class UserQueryRepository(TenantDbContext dbContext) : IUserQueryRepository
{
    public async Task<List<User>> GetAllAsync() => await dbContext.Users
                                                                     .Include(u => u.Role)
                                                                     .ToListAsync();

    public async Task<User?> GetByEmailAsync(string email) => await dbContext.Users
                                                                           .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(UserId userId) => await dbContext.Users.FindAsync(userId);

    public async Task<Role?> GetUserRole(UserId userId) => await dbContext.Users
                                                                             .Where(u => u.Id == userId)
                                                                             .Select(u => u.Role)
                                                                             .FirstOrDefaultAsync();

}
