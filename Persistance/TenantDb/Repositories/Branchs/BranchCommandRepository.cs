
using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.Repositories;

namespace Persistence.TenantDb.Repositories.Branchs;

public class BranchCommandRepository(
       TenantDbContext dbContext)
    : IBranchCommandRepository
{
    public void AddAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        dbContext.Branches.Add(branch);
    }

    public void DeleteAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        dbContext.Branches.Remove(branch);
    }

    public void DeleteListAsync(List<Branch> branchs, CancellationToken cancellationToken = default)
    {
        dbContext.Branches.RemoveRange(branchs);
    }

    public void UpdateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
