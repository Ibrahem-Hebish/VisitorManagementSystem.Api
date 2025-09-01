using Domain.TenantDomain.Tokens;
using Domain.TenantDomain.Tokens.ValueObjects;

namespace Domain.TenantDomain.Tokens.Repositories;

public interface IUserTokenQueryRepository
{
    Task<UserToken?> GetByIdAsync(UserTokenId userTokenId, CancellationToken cancellationToken = default);
    Task<List<UserToken>> GetAllAsync();

}