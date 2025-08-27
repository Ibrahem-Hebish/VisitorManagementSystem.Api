using Domain.Buildings.ObjectValues;

namespace Domain.Buildings.Repositories;

public interface ICommandBuildingRepository
{
    Task AddAsync(Building building);
    Task UpdateAsync(Building building);
    Task DeleteAsync(BuildingId buildingId);

}
