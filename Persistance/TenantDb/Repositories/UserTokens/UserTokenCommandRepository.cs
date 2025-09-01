using Domain.TenantDomain.Tokens;
using Domain.TenantDomain.Tokens.Repositories;

namespace Persistence.TenantDb.Repositories.UserTokens;

public class UserTokenCommandRepository(TenantDbContext dbContext) : IUserTokenCommandRepository
{
    public async Task AddAsync(UserToken userToken) => await dbContext.UserTokens.AddAsync(userToken);


    public void DeleteAsync(UserToken userToken) => dbContext.UserTokens.Remove(userToken);


    public void UpdateAsync(UserToken userToken) => dbContext.UserTokens.Update(userToken);

}
