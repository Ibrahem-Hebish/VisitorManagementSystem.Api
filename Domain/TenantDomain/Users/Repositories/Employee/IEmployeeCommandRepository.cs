namespace Domain.TenantDomain.Users.Repositories.Employees;

public interface IEmployeeCommandRepository
{
    Task AddAsync(Employee employee);
    void Update(Employee employee);
    void Delete(Employee employee);
}
