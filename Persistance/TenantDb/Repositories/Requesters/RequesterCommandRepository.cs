using Azure.Core;
using Domain.Users;
using Domain.Users.Repositories.Requesters;

namespace Persistence.TenantDb.Repositories.Requesters;

public class RequesterCommandRepository(TenantDbContext dbContext) : IRequesterCommandRepository
{
    public async Task AddAsync(Requester requester) => await dbContext.Requesters.AddAsync(requester);


    public void DeleteAsync(Requester requester) => dbContext.Requesters.Remove(requester);



    public void UpdateAsync(Requester requester) =>  dbContext.Requesters.Update(requester);
    
}
