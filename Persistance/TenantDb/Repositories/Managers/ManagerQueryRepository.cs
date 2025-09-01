using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.Users.Repositories.Managers;

namespace Persistence.TenantDb.Repositories.Managers;

public class ManagerQueryRepository(TenantDbContext dbContext) : IManagerQueryRepository
{

    public async Task<Manager?> GetByIdAsync(UserId userId) => await dbContext.Managers.FindAsync(userId);

    public Task<List<Manager>> GetAllAsync() => dbContext.Managers
                                                    .Include(e => e.Branch)
                                                    .Include(e => e.Role)
                                                    .ToListAsync();

}
