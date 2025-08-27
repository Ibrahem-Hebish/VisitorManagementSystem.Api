namespace Domain.Users.Repositories.Managers;

public interface IManagerQueryRepository
{
    Task<Manager?> GetByIdAsync(UserId userId);
    Task<List<Manager>> GetAllAsync();
}
