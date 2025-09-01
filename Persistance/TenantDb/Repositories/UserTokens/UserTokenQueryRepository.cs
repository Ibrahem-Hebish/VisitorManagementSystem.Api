using Domain.TenantDomain.Tokens;
using Domain.TenantDomain.Tokens.Repositories;
using Domain.TenantDomain.Tokens.ValueObjects;

namespace Persistence.TenantDb.Repositories.UserTokens;

public class UserTokenQueryRepository(TenantDbContext dbContext) : IUserTokenQueryRepository
{
    public async Task<List<UserToken>> GetAllAsync() => await dbContext.UserTokens.ToListAsync();

    public async Task<UserToken?> GetByIdAsync(UserTokenId userTokenId,
        CancellationToken cancellationToken = default) => await dbContext.UserTokens.FindAsync(userTokenId);

}