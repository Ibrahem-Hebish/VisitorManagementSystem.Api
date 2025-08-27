namespace Domain.Users.Repositories.Users;

public interface IUserCommandRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(UserId userId);

}
