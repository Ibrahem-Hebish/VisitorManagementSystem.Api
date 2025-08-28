using Domain.Buildings.Repositories;

namespace Persistence.TenantDb.Repositories.Buildings;

public class BuildingQueryRepository(TenantDbContext dbContext) : IBuildingQueryRepository
{
    public async Task<bool> ExsistsAsync(BuildingId buildingId)
    {
        return await dbContext.Buildings.AnyAsync(b => b.Id == buildingId);
    }

    public Task<List<Building>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<Building>> GetByBranchIdAsync(BranchId branchId)
    {
        throw new NotImplementedException();
    }

    public Task<Building?> GetByIdAsync(BuildingId buildingId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetFloorsCountAsync(BuildingId buildingId)
    {
        var building = await dbContext.Buildings.FindAsync(buildingId);

        if (building is null)
            return 0;

        return building.FloorNumbers;
    }
}
