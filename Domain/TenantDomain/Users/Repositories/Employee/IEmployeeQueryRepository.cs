using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users.Repositories.Employees;

public interface IEmployeeQueryRepository
{
    Task<Employee?> GetByIdAsync(UserId userId);
    Task<List<Employee>> GetAllAsync();
}
