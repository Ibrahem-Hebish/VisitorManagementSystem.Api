namespace Domain.Buildings.Repositories;

public interface IBuildingQueryRepository
{
    Task<Building?> GetByIdAsync(BuildingId buildingId);
    Task<bool> ExsistsAsync(BuildingId buildingId);
    Task<int> GetFloorsCountAsync(BuildingId buildingId);
    Task<List<Building>> GetAllAsync();
    Task<List<Building>> GetByBranchIdAsync(BranchId branchId);
}
