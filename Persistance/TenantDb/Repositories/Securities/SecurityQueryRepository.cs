using Domain.Users.Repositories.Securities;

namespace Persistence.TenantDb.Repositories.Securities;

public class SecurityQueryRepository(TenantDbContext dbContext) : ISecurityQueryRepository
{
    public async Task<Security?> GetByIdAsync(UserId userId) => await dbContext.Securities.FindAsync(userId);

    public Task<List<Security>> GetAllAsync() => dbContext.Securities
                                                    .Include(e => e.Branch)
                                                    .Include(e => e.Role)
                                                    .ToListAsync();

}
