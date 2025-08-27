using Domain.Buildings.ObjectValues;

namespace Domain.Buildings.Repositories;

public interface IQueryBuildingRepository
{
    Task<Building?> GetByIdAsync(BuildingId buildingId);
    Task<List<Building>> GetAllAsync();
    Task<List<Building>> GetByBranchIdAsync(BranchId branchId);
}
