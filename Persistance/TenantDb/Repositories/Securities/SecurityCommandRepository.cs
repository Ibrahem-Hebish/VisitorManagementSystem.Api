using Domain.Users.Repositories.Securities;

namespace Persistence.TenantDb.Repositories.Securities;

public class SecurityCommandRepository(TenantDbContext dbContext) : ISecurityCommandRepository
{
    public async Task AddAsync(Security security) => await dbContext.Securities.AddAsync(security);
    public void DeleteAsync(Security security) => dbContext.Securities.Remove(security);
    public void UpdateAsync(Security security) => dbContext.Securities.Update(security);

}
