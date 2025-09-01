using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Buildings.ObjectValues;

namespace Domain.TenantDomain.Buildings.Repositories;

public interface IQueryBuildingRepository
{
    Task<Building?> GetByIdAsync(BuildingId buildingId);
    Task<List<Building>> GetAllAsync();
    Task<List<Building>> GetByBranchIdAsync(BranchId branchId);
}
