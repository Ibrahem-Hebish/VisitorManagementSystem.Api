using Domain.TenantDomain.Attachments;
using Domain.TenantDomain.Belongings;
using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits.DomainErrors;
using Domain.TenantDomain.Permits.DomainEvents;
using Domain.TenantDomain.Permits.Enums;
using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.PermitTracks;
using Domain.TenantDomain.PermitTracks.Enums;
using Domain.TenantDomain.PermitUpdateRequests;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.VisitorPermits;
using Domain.TenantDomain.Visitors;

namespace Domain.TenantDomain.Permits;

public class Permit : AggregateRoot, IMultiTenant
{
    public PermitId PermitId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Reason { get; private set; }
    public PermitStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; }
    public BuildingId? BuildingId { get; set; }
    public Building? Building { get; set; }
    public int? FloorNumber { get; set; }
    public UserId RequestedBy { get; private set; }
    public Requester Requester { get; private set; } = null!;
    public UserId? HandledBy { get; private set; }
    public Manager? Handler { get; private set; }

    private readonly List<Attachment> _attachments = [];
    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();

    private readonly List<Belonging> _belongings = [];
    public IReadOnlyCollection<Belonging> Belongings => _belongings.AsReadOnly();
    private readonly List<PermitTrack> _permitTracks = [];
    public IReadOnlyCollection<PermitTrack> PermitTracks => _permitTracks.AsReadOnly();
    private readonly List<PermitUpdateRequest> _permitUpdateRequests = [];
    public IReadOnlyCollection<PermitUpdateRequest> PermitUpdateRequest => _permitUpdateRequests.AsReadOnly();
    private readonly List<Visitor> _visitors = [];
    public IReadOnlyCollection<Visitor> Visitors => _visitors.AsReadOnly();

    private readonly List<VisitorPermit> _vistorPermits = [];
    public IReadOnlyCollection<VisitorPermit> VisitorPermits => _vistorPermits.AsReadOnly();
    private Permit()
    {
    }
    private Permit(DateTime startDate, DateTime endDate,
        string reason, BuildingId buildingId,
        int floorNumber, UserId requesterId,
        List<Visitor> visitors)
    {
        PermitId = new PermitId(Guid.NewGuid());
        StartDate = startDate;
        EndDate = endDate;
        Reason = reason;
        Status = PermitStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        BuildingId = buildingId;
        FloorNumber = floorNumber;
        RequestedBy = requesterId;
        _visitors = visitors;

        Raise(new NewPermitCreatedDomainEvent(PermitId, GetVisitorsEmails()));

        var permitTrack = PermitTrack.Create(PermitTrackAction.Created, Reason, PermitId, RequestedBy);

        AddPermitTrack(permitTrack);
    }
    public static Permit Create(DateTime startDate, DateTime endDate,
               string reason, BuildingId buildingId,
               int floorNumber, UserId requestedBy, List<Visitor> visitors)
    {
        return new(startDate, endDate, reason, buildingId, floorNumber, requestedBy, visitors);
    }



    public void ExtendEndDate(DateTime newEndDate)
    {
        if (newEndDate <= EndDate)
            throw new InvalidExtendedDate("New end date must be later than the current end date.");

        EndDate = newEndDate;
        UpdatedAt = DateTime.UtcNow;

        Raise(new PermitExtendedDomainEvent(PermitId, EndDate, GetVisitorsEmails()));

        var permitTrack = PermitTrack.Create(PermitTrackAction.Extended, $"Permit Extended to {newEndDate}", PermitId, HandledBy!);

        AddPermitTrack(permitTrack);

    }

    public void Cancel()
    {
        if (Status == PermitStatus.Cancelled)
            throw new InvalidOperationException("Permit is already cancelled.");

        Status = PermitStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;

        Raise(new PermitCanceledDomainEvent(PermitId, GetVisitorsEmails()));

        var permitTrack = PermitTrack.Create(PermitTrackAction.Cancled, $"Permit canceled", PermitId, HandledBy!);

        AddPermitTrack(permitTrack);

    }
    public void Approve(UserId managerId)
    {
        if (Status != PermitStatus.Pending)
            throw new InvalidOperationException("Only pending permits can be approved.");

        Status = PermitStatus.Approved;
        HandledBy = managerId;
        UpdatedAt = DateTime.UtcNow;

        Raise(new PermitApprovedDomainEvent(PermitId, GetVisitorsEmails()));

        var permitTrack = PermitTrack.Create(PermitTrackAction.Extended, $"Permit Approved at {DateTime.UtcNow}", PermitId, HandledBy!);

        AddPermitTrack(permitTrack);
    }

    public void Reject(UserId managerId)
    {
        if (Status != PermitStatus.Pending)
            throw new InvalidOperationException("Only pending permits can be rejected.");

        Status = PermitStatus.Rejected;
        HandledBy = managerId;
        UpdatedAt = DateTime.UtcNow;

        Raise(new PermitRejectedDomainEvent(PermitId, GetVisitorsEmails()));

        var permitTrack = PermitTrack.Create(PermitTrackAction.Extended, $"Permit rejected at {DateTime.UtcNow}", PermitId, HandledBy!);

        AddPermitTrack(permitTrack);

    }

    public void Expire(Manager manager)
    {
        if (DateTime.UtcNow > EndDate)
        {
            Status = PermitStatus.Expired;
            HandledBy = manager.Id;
            UpdatedAt = DateTime.UtcNow;

            Raise(new PermitExpiredDomainEvent(PermitId, GetVisitorsEmails()));

            var permitTrack = PermitTrack.Create(PermitTrackAction.Extended, $"Permit expired at {DateTime.UtcNow}", PermitId, HandledBy!);

            AddPermitTrack(permitTrack);
        }
    }

    public void AddAttachment(Attachment attachment) => _attachments.Add(attachment);
    public void AddAttachmentList(List<Attachment> attachments) => _attachments.AddRange(attachments);
    public void RemoveAttachment(Attachment attachment) => _attachments.Remove(attachment);
    public void AddBelonging(Belonging belonging) => _belongings.Add(belonging);
    public void AddBelongingList(List<Belonging> belongings) => _belongings.AddRange(belongings);
    public void RemoveBelonging(Belonging belonging) => _belongings.Remove(belonging);
    public void AddPermitTrack(PermitTrack permitTrack) => _permitTracks.Add(permitTrack);
    public void RemovePermitTrack(PermitTrack permitTrack) => _permitTracks.Remove(permitTrack);
    public void AddPermitUpdateRequest(PermitUpdateRequest permitUpdateRequest) => _permitUpdateRequests.Add(permitUpdateRequest);
    public void RemovePermitUpdateRequest(PermitUpdateRequest permitUpdateRequest) => _permitUpdateRequests.Remove(permitUpdateRequest);
    private List<string> GetVisitorsEmails() => [.. _visitors.Select(v => v.Email)];


}
