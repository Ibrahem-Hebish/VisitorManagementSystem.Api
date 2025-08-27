using Domain.Permits.Repositories;

namespace Persistence.TenantDb.Repositories.Permits;

public class PermitCommandRepository(TenantDbContext dbContext) : IPermitCommandRepository
{
    public async Task CreateAsync(Permit permit, CancellationToken cancellationToken = default)
                                                    => await dbContext.Permits.AddAsync(permit, cancellationToken);


    public void DeleteAsync(Permit permit) => dbContext.Permits.Remove(permit);


    public void UpdateAsync(Permit permit) => dbContext.Permits.Update(permit);

}
