using Domain.Tokens.ValueObjects;

namespace Domain.Tokens.Repositories;

public interface IUserTokenQueryRepository
{
    Task<UserToken?> GetByIdAsync(UserTokenId userTokenId, CancellationToken cancellationToken = default);
    Task<List<UserToken>> GetAllAsync();

}