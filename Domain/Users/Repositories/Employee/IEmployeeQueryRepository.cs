namespace Domain.Users.Repositories.Employees;

public interface IEmployeeQueryRepository
{
    Task<Employee?> GetByIdAsync(UserId userId);
    Task<List<Employee>> GetAllAsync();
}
