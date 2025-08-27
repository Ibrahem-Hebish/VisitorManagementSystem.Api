using Domain.Users.Repositories.Requesters;

namespace Persistence.TenantDb.Repositories.Requesters;

public class RequesterQueryRepository(TenantDbContext dbContext) : IRequesterQueryRepository
{

    public async Task<Requester?> GetByIdAsync(UserId userId) => await dbContext.Requesters.FindAsync(userId);
    public Task<List<Requester>> GetAllAsync() => dbContext.Requesters
                                                    .Include(e => e.Branch)
                                                    .Include(e => e.Role)
                                                    .ToListAsync();

}
