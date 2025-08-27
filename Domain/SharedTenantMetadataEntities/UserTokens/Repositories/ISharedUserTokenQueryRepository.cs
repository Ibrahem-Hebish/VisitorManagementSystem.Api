using Domain.SharedTenantMetadataEntities.UserTokens.ObjectValues;

namespace Domain.SharedTenantMetadataEntities.UserTokens.Repositories;

public interface ISharedUserTokenQueryRepository
{
    Task<SharedUserToken?> GetByIdAsync(SharedUserTokenId id, CancellationToken cancellationToken = default);
    Task<SharedUserToken?> GetByIdWithBranchAsync(SharedUserTokenId id, CancellationToken cancellationToken = default);
}
