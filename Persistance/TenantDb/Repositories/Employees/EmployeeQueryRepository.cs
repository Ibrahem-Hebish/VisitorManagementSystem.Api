
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.Users.Repositories.Employees;

namespace Persistence.TenantDb.Repositories.Employees;

public class EmployeeQueryRepository(TenantDbContext dbContext) : IEmployeeQueryRepository
{
    public Task<List<Employee>> GetAllAsync() => dbContext.Employees
                                                    .Include(e => e.Branch)
                                                    .Include(e => e.Role)
                                                    .ToListAsync();

    public Task<IEnumerable<Employee>> GetByBranchIdAsync(BranchId branchId)
    {
        var employees = dbContext.Employees
            .Where(e => e.BranchId == branchId)
            .Include(e => e.Branch)
            .Include(e => e.Role)
            .AsEnumerable();

        return Task.FromResult(employees);
    }

    public async Task<Employee?> GetByIdAsync(UserId userId) => await dbContext.Employees.FindAsync(userId);
}
