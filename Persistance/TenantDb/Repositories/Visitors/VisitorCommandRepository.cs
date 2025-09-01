
using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.Repositories;

namespace Persistence.TenantDb.Repositories.Visitors;

public class VisitorCommandRepository(TenantDbContext dbContext) : IVisitorCommandRepository
{
    public async Task AddAsync(Visitor visitor)
                       => await dbContext.AddAsync(visitor);

    public void Delete(Visitor visitor)
                          => dbContext.Visitors.Remove(visitor);

    public void Update(Visitor visitor)
                        => dbContext.Visitors.Update(visitor);
}
