namespace Domain.Users.Repositories.Employees;

public interface IEmployeeCommandRepository
{
    Task AddAsync(Employee employee);
    void UpdateAsync(Employee employee);
    void DeleteAsync(Employee employee);
}
