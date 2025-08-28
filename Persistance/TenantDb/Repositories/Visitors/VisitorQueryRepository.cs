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

    public Task<List<Permit>> GetVisitorPermitsAsync(VisitorId visitorId)
    {
        throw new NotImplementedException();
    }
}
