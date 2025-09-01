using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.Repositories.Employees;

namespace Persistence.TenantDb.Repositories.Employees;

public class EmpolyeeCommandRepository(TenantDbContext dbContext) : IEmployeeCommandRepository
{
    public async Task AddAsync(Employee employee) => await dbContext.AddAsync(employee);


    public void Delete(Employee employee) => dbContext.Employees.Remove(employee);



    public void Update(Employee employee) => dbContext.Employees.Update(employee);

}
