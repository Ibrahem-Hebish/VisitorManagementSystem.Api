using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits;

namespace Domain.TenantDomain.Buildings;

public class Building : Entity, IMultiTenant
{
    public BuildingId Id { get; private set; }
    public string Name { get; private set; }
    public int FloorNumbers { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; }
    private readonly List<Permit> permits = [];
    public IReadOnlyCollection<Permit> Permits => permits.AsReadOnly();

    private Building() { }
    private Building(string name, int floorNumbers)
    {
        Id = new(Guid.NewGuid());
        Name = name;
        FloorNumbers = floorNumbers;

    }

    public static Building Create(string name, int floorNumbers)
        => new(name, floorNumbers);

    public void UpdateName(string name) => Name = name;
    public void UpdateFloorNumbers(int floorNumbers) => FloorNumbers = floorNumbers;
}
