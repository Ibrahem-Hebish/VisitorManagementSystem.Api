using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Buildings.Repositories;

namespace Persistence.TenantDb.Repositories.Buildings;

public class BuildingQueryRepository(TenantDbContext dbContext) : IBuildingQueryRepository
{
    public async Task<bool> ExsistsAsync(BuildingId buildingId)
    {
        return await dbContext.Buildings.AnyAsync(b => b.Id == buildingId);
    }

    public async Task<List<Building>> GetAllAsync()
    {
        return await dbContext.Buildings.ToListAsync();
    }

    public Task<List<Building>> GetByBranchIdAsync(BranchId branchId)
    {
        throw new NotImplementedException();
    }

    public async Task<Building?> GetByIdAsync(BuildingId buildingId)
    {
        return await dbContext.Buildings.FindAsync(buildingId);
    }

    public async Task<int> GetFloorsCountAsync(BuildingId buildingId)
    {
        var building = await dbContext.Buildings.FindAsync(buildingId);

        if (building is null)
            return 0;

        return building.FloorNumbers;
    }
}
