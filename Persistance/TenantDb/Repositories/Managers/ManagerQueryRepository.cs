using Domain.Users.Repositories.Managers;

namespace Persistence.TenantDb.Repositories.Managers;

public class ManagerQueryRepository(TenantDbContext dbContext) : IManagerQueryRepository
{

    public async Task<Manager?> GetByIdAsync(UserId userId) => await dbContext.Managers.FindAsync(userId);

    public Task<List<Manager>> GetAllAsync() => dbContext.Managers
                                                    .Include(e => e.Branch)
                                                    .Include(e => e.Role)
                                                    .ToListAsync();

}
