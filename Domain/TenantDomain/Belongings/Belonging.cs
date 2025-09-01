using Domain.TenantDomain.Belongings.ObjectValues;
using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.ObjectValues;

namespace Domain.TenantDomain.Belongings;

public class Belonging : Entity, IMultiTenant
{
    public BelongingId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; } = null!;
    public PermitId PermitId { get; private set; }
    public Permit Permit { get; private set; } = null!;

    private Belonging() { }
    public Belonging(string name, string description, PermitId permitId)
    {
        Id = new BelongingId(Guid.NewGuid());
        Name = name;
        Description = description;
        PermitId = permitId;
    }

}
