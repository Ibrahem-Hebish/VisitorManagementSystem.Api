namespace Domain.Roles.Repositories;

public interface IRoleQueryRepository
{
    Task<Role?> GetRoleByName(string name);
}
