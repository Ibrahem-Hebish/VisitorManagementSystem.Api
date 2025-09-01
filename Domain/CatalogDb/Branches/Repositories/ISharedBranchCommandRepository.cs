namespace Domain.CatalogDb.Branches.Repositories;

public interface ISharedBranchCommandRepository
{
    Task AddAsync(SharedBranch branch, CancellationToken cancellationToken = default);

    Task UpdateAsync(SharedBranch branch, CancellationToken cancellationToken = default);

    Task DeleteAsync(SharedBranch branch, CancellationToken cancellationToken = default);
}
