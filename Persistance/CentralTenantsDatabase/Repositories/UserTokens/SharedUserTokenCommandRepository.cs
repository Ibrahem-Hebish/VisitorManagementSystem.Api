
namespace Persistence.CentralTenantsDatabase.Repositories.UserTokens;

public class SharedUserTokenCommandRepository : ISharedUserTokenCommandRepository
{
    private readonly SharedDbContext _dbContext;

    public SharedUserTokenCommandRepository(SharedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(SharedUserToken userToken)
    {
        await _dbContext.SharedUserTokens.AddAsync(userToken);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(SharedUserToken userToken)
    {
        _dbContext.SharedUserTokens.Remove(userToken);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(SharedUserToken userToken)
    {
        _dbContext.SharedUserTokens.Update(userToken);
        await _dbContext.SaveChangesAsync();
    }
}