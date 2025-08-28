using Domain.Visitors.Repositories;

namespace Persistence.TenantDb.Repositories.Visitors;

public class VisitorQueryRepository(TenantDbContext dbContext) : IVisitorQueryRepository
{
    public async Task<bool> ExsistsAsync(VisitorId visitorId)
    {
        return await dbContext.Visitors.AnyAsync(v => v.VisitorId == visitorId);
    }

    public Task<List<Visitor>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Visitor>> GetByBranchIdAsync(BranchId branchId)
    {
        throw new NotImplementedException();
    }

    public Task<Visitor?> GetByIdAsync(VisitorId visitorId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Permit>> GetVisitorPermits(VisitorId visitorId)
    {
        var visitor = await dbContext.Visitors.FindAsync(visitorId);

        if (visitor is not null)
        {
            await dbContext.Entry(visitor).Collection(x => x.Permits).LoadAsync();

            if (visitor.Permits.Count == 1)
                return [];

            return [.. visitor.Permits];
        }
        return [];

    }

    public Task<List<Permit>> GetVisitorPermitsAsync(VisitorId visitorId)
    {
        throw new NotImplementedException();
    }
}
