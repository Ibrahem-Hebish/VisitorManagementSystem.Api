
using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Branches.Repositories;
using Domain.TenantDomain.Tenants.ObjectValues;

namespace Persistence.TenantDb.Repositories.Branchs;

public class BranchQueryRepository(TenantDbContext dbContext) : IBranchQueryRepository
{
    public async Task<List<Branch>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Branches.ToListAsync(cancellationToken);
    }

    public async Task<Branch?> GetByIdAsync(BranchId id, CancellationToken cancellationToken = default)
    {
        var branch = await dbContext.Branches.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (branch is not null)
            await dbContext.Entry(branch).Collection(b => b.Employees).LoadAsync(cancellationToken);

        return branch;
    }

    public async Task<List<Branch>> GetByTenantIdAsync(TenantId id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Branches.Where(b => b.TenantId == id)
            .Include(b => b.Employees)
            .ToListAsync();
    }

    public async Task<Branch?> GetDetailsAsync(BranchId id, CancellationToken cancellationToken = default)
    {
        var branch = await dbContext.Branches
                                     .Include(b => b.Employees)
                                      .ThenInclude(e => e.Role)
                                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return branch;

    }
}
