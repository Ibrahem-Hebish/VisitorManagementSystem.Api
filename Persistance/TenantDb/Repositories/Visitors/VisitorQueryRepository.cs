using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.ObjectValues;
using Domain.TenantDomain.Visitors.Repositories;

namespace Persistence.TenantDb.Repositories.Visitors;

public class VisitorQueryRepository(TenantDbContext dbContext) : IVisitorQueryRepository
{
    public async Task<bool> ExsistsAsync(VisitorId visitorId)
    {
        return await dbContext.Visitors.AnyAsync(v => v.Id == visitorId);
    }

    public async Task<List<Visitor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Visitors.ToListAsync(cancellationToken: cancellationToken);
    }

    public Task<IEnumerable<Visitor>> GetByBranchIdAsync(BranchId branchId)
    {
        throw new NotImplementedException();
    }

    public async Task<Visitor?> GetByIdAsync(VisitorId visitorId)
    {
        return await dbContext.Visitors.FindAsync(visitorId);
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

    public async Task<List<Visitor>> GetExsistingVisitors(List<string> emails, CancellationToken cancellationToken = default)
    {
        return await dbContext.Visitors
                  .Where(v => emails.Contains(v.Email))
                  .ToListAsync(cancellationToken);
    }

    public Task<List<Permit>> GetVisitorPermitsAsync(VisitorId visitorId)
    {
        throw new NotImplementedException();
    }

    public async Task<Visitor?> GetByEmailAsync(string email)
    {
        return await dbContext.Visitors.FirstOrDefaultAsync(x => x.Email == email);
    }
}
