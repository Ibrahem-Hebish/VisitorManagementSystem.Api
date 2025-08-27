namespace Domain.Users.Repositories.Securities;

public interface ISecurityQueryRepository
{
    Task<Security?> GetByIdAsync(UserId userId);
    Task<List<Security>> GetAllAsync();
}
