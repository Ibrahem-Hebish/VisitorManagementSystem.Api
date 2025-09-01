using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users.Repositories.Requesters;

public interface IRequesterQueryRepository
{
    Task<Requester?> GetByIdAsync(UserId userId);
    Task<List<Requester>> GetAllAsync();
}
