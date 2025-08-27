using Domain.EntryLogs.ObjectValues;

namespace Domain.EntryLogs;

public class Entrylog : Entity, IMultiTenant
{
    public EntrylogId Id { get; private set; }
    public DateTime EntryTime { get; private set; }
    public bool IsInside { get; private set; }
    public UserId AllowedBy { get; private set; }
    public Security Employee { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; }
    public VisitorId VisitorId { get; private set; }
    public Visitor Visitor { get; private set; } = null!;
    public PermitId PermitId { get; private set; }
    public Permit Permit { get; private set; }

    private Entrylog() { }
    private Entrylog(EntrylogId id, DateTime entryTime, UserId allowedBy, Visitor visitor)
    {
        Id = id;
        EntryTime = entryTime;
        IsInside = true;
        AllowedBy = allowedBy;
        Visitor = visitor;
    }

    public static Entrylog Create(EntrylogId id, DateTime entryTime, UserId allowedBy, Visitor visitor)
                                                                      => new(id, entryTime, allowedBy, visitor);
}
