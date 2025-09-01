namespace Persistence.CentralTenantsDatabase.Repositories.UserTokens;

public class SharedUserTokenQueryRepository : ISharedUserTokenQueryRepository
{
    private readonly SharedDbContext _dbContext;

    public SharedUserTokenQueryRepository(SharedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SharedUserToken?> GetByIdAsync(SharedUserTokenId id, CancellationToken cancellationToken = default)
    {
        var userToken = await _dbContext.SharedUserTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return userToken;
    }

    public async Task<SharedUserToken?> GetByIdWithBranchAsync(SharedUserTokenId id, CancellationToken cancellationToken = default)
    {
        var userToken = await _dbContext.SharedUserTokens
             .AsNoTracking()
             .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (userToken is not null)
            await _dbContext.Entry(userToken).Reference(u => u.SharedBranch).LoadAsync(cancellationToken);

        return userToken;
    }
}
