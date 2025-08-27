using Domain.PermitUpdateRequests.Enums;
using Domain.PermitUpdateRequests.ObjectValues;

namespace Domain.PermitUpdateRequests;

public class PermitUpdateRequest : Entity, IMultiTenant
{
    public PermitUpdateRequestId Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Description { get; private set; }

    public PermitUpdateAction PermitUpdateAction { get; private set; }
    public PermitId PermitId { get; private set; }
    public Permit Permit { get; private set; }
    public UserId RequesterId { get; private set; }
    public Requester Requester { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; }

    private PermitUpdateRequest()
    {
    }
    private PermitUpdateRequest(PermitUpdateAction permitUpdateAction, PermitId permitId, UserId requesterId, string description)
    {
        Id = new(Guid.NewGuid());
        PermitUpdateAction = permitUpdateAction;
        PermitId = permitId;
        RequesterId = requesterId;
        Description = description;
    }

    public static PermitUpdateRequest Create(PermitUpdateAction permitUpdateAction, PermitId permitId, UserId requesterId, string description)
    {
        return new(permitUpdateAction, permitId, requesterId, description);
    }
}
