namespace Domain.SharedTenantMetadataEntities.SharedUsers.Repositories;

public interface ISharedUserQueryRepository
{
    Task<SharedUser?> GetByIdAsync(SharedUserId id, CancellationToken cancellationToken = default);
    Task<List<SharedUser>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SharedUser?> GetByEmailAsync(string email);
}
