
using Domain.TenantDomain.Roles;
using Domain.TenantDomain.Roles.Repositories;

namespace Persistence.TenantDb.Repositories.Roles;

public class RoleQueryRepository(TenantDbContext tenantDbContext) : IRoleQueryRepository
{
    public async Task<Role?> GetRoleByName(string name) =>
                                               await tenantDbContext.Roles
                                                                          .FirstOrDefaultAsync(x => x.Name == name);

}
