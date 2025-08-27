namespace Domain.Branches.Repositories;

public interface IBranchCommandRepository
{
    void AddAsync(Branch branch, CancellationToken cancellationToken = default);

    void UpdateAsync(Branch branch, CancellationToken cancellationToken = default);

    void DeleteAsync(Branch branch, CancellationToken cancellationToken = default);

    void DeleteListAsync(List<Branch> branchs, CancellationToken cancellationToken = default);

}
