using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Buildings.ObjectValues;

namespace Domain.TenantDomain.Buildings.Repositories;

public interface IBuildingCommandRepository
{
    Task AddAsync(Building building);
    void Update(Building building);
    void Delete(Building building);
    Task SetPermitsBuildingIdToNull(BuildingId buildingId);

}
