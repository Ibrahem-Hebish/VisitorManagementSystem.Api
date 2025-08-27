using Domain.Users.Repositories.Employees;

namespace Persistence.TenantDb.Repositories.Employees;

public class EmpolyeeCommandRepository(TenantDbContext dbContext) : IEmployeeCommandRepository
{
    public async Task AddAsync(Employee employee) => await dbContext.AddAsync(employee);


    public void DeleteAsync(Employee employee) => dbContext.Employees.Remove(employee);



    public void UpdateAsync(Employee employee) => dbContext.Employees.Update(employee);

}
