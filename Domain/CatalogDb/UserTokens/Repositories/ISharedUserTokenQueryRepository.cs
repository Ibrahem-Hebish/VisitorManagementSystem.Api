using Domain.CatalogDb.UserTokens.ObjectValues;

namespace Domain.CatalogDb.UserTokens.Repositories;

public interface ISharedUserTokenQueryRepository
{
    Task<SharedUserToken?> GetByIdAsync(SharedUserTokenId id, CancellationToken cancellationToken = default);
    Task<SharedUserToken?> GetByIdWithBranchAsync(SharedUserTokenId id, CancellationToken cancellationToken = default);
}
