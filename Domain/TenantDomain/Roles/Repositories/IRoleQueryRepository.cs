using Domain.TenantDomain.Roles;

namespace Domain.TenantDomain.Roles.Repositories;

public interface IRoleQueryRepository
{
    Task<Role?> GetRoleByName(string name);
}
