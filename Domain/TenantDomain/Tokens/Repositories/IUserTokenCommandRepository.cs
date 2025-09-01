using Domain.TenantDomain.Tokens;

namespace Domain.TenantDomain.Tokens.Repositories;

public interface IUserTokenCommandRepository
{
    Task AddAsync(UserToken userToken);
    void UpdateAsync(UserToken userToken);
    void DeleteAsync(UserToken userToken);
}
