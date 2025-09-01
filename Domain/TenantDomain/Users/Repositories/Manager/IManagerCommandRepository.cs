namespace Domain.TenantDomain.Users.Repositories.Managers;

public interface IManagerCommandRepository
{
    Task AddAsync(Manager employee);
    void UpdateAsync(Manager employee);
    void DeleteAsync(Manager employee);
}
