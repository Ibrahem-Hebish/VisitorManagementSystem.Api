namespace Domain.SharedTenantMetadataEntities.SharedUsers.Repositories;

public interface ISharedUserCommandRepository
{
    Task AddAsync(SharedUser admin, CancellationToken cancellationToken = default);
    Task UpdateAsync(SharedUser admin, CancellationToken cancellationToken = default);
    Task DeleteAsync(SharedUser admin, CancellationToken cancellationToken = default);
}
