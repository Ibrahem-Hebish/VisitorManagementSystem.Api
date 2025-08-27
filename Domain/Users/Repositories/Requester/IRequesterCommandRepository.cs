namespace Domain.Users.Repositories.Requesters;

public interface IRequesterCommandRepository
{
    Task AddAsync(Requester employee);
    void UpdateAsync(Requester employee);
    void DeleteAsync(Requester employee);
}
