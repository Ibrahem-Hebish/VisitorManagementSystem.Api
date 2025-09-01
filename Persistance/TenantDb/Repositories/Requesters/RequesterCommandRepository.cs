using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.Repositories.Requesters;

namespace Persistence.TenantDb.Repositories.Requesters;

public class RequesterCommandRepository(TenantDbContext dbContext) : IRequesterCommandRepository
{
    public async Task AddAsync(Requester requester) => await dbContext.Requesters.AddAsync(requester);


    public void DeleteAsync(Requester requester) => dbContext.Requesters.Remove(requester);



    public void UpdateAsync(Requester requester) => dbContext.Requesters.Update(requester);

}
