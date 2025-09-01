using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Buildings.Repositories;

namespace Persistence.TenantDb.Repositories.Buildings;

public class BuildingCommandRepository(TenantDbContext dbContext) : IBuildingCommandRepository
{
    public async Task AddAsync(Building building) => await dbContext.Buildings.AddAsync(building);


    public void Delete(Building building) => dbContext.Buildings.Remove(building);


    public void Update(Building building) => dbContext.Buildings.Update(building);

    public async Task SetPermitsBuildingIdToNull(BuildingId buildingId)
    {
        await dbContext.Permits
                            .Where(p => p.BuildingId == buildingId)
                            .ExecuteUpdateAsync(b => b
                                .SetProperty(p => p.BuildingId, p => null)
                                .SetProperty(p => p.FloorNumber, p => null));
    }

}
