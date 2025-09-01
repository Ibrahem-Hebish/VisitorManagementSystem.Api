using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.EntryLogs.DomainEvents;
using Domain.TenantDomain.EntryLogs.ObjectValues;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.EntryLogs;

public class Entrylog : Entity, IMultiTenant
{
    public EntrylogId Id { get; private set; }
    public DateTime EntryTime { get; private set; }
    public bool IsInside { get; private set; }
    public UserId AllowedBy { get; private set; }
    public Security Employee { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; }
    public PermitId PermitId { get; private set; }
    public Permit Permit { get; private set; }

    private Entrylog() { }
    private Entrylog(UserId allowedBy, PermitId permitId)
    {
        Id = new EntrylogId(Guid.NewGuid());
        EntryTime = DateTime.UtcNow;
        IsInside = true;
        AllowedBy = allowedBy;
        PermitId = permitId;

        Raise(new PermitTimeHasStartedDomainEvent(permitId));
    }

    public static Entrylog Create(UserId allowedBy, PermitId permitId)
                                                                      => new(allowedBy, permitId);

}
