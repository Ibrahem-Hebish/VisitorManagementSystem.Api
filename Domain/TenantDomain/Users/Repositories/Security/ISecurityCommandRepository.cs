namespace Domain.TenantDomain.Users.Repositories.Securities;

public interface ISecurityCommandRepository
{
    Task AddAsync(Security employee);
    void UpdateAsync(Security employee);
    void DeleteAsync(Security employee);
}
