namespace Domain.Users.Repositories.Requesters;

public interface IRequesterQueryRepository
{
    Task<Requester?> GetByIdAsync(UserId userId);
    Task<List<Requester>> GetAllAsync();
}
