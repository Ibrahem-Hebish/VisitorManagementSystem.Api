using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.Repositories.Managers;

namespace Persistence.TenantDb.Repositories.Managers;

public class ManagerCommandRepository(TenantDbContext dbContext) : IManagerCommandRepository
{
    public async Task AddAsync(Manager manager) => await dbContext.Managers.AddAsync(manager);
    public void DeleteAsync(Manager manager) => dbContext.Managers.Remove(manager);
    public void UpdateAsync(Manager manager) => dbContext.Employees.Update(manager);

}
