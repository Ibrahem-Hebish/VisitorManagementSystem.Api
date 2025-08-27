namespace Persistence.CentralTenantsDatabase.Repositories.Branch;

public class SharedBranchCommandRepository(SharedDbContext dbContext) : ISharedBranchCommandRepository
{
    private readonly SharedDbContext _dbContext = dbContext;

    public async Task AddAsync(SharedBranch branch, CancellationToken cancellationToken = default)
    {
        await _dbContext.SharedBranches.AddAsync(branch, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SharedBranch branch, CancellationToken cancellationToken = default)
    {
        _dbContext.SharedBranches.Update(branch);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SharedBranch branch, CancellationToken cancellationToken = default)
    {
        _dbContext.SharedBranches.Remove(branch);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

