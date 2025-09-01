namespace Persistence.CentralTenantsDatabase.Repositories.Branch;

public class SharedBranchQueryRepository(SharedDbContext dbContext) : ISharedBranchQueryRepository
{
    public async Task<List<SharedBranch>> GetAllAsync(CancellationToken cancellationToken = default)
                                                         => await dbContext.SharedBranches
                                                                            .AsNoTracking()
                                                                            .ToListAsync(cancellationToken);

    public Task<SharedBranch?> GetByIdAsync(SharedBranchId? id, CancellationToken cancellationToken = default)
    {
        if (id is null)
            return null!;

        var branch = dbContext.SharedBranches
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return branch;
    }
}

