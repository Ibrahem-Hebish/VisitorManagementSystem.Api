using Domain.PermitTracks.Enums;

namespace Domain.PermitTracks;

public class PermitTrack : Entity, IMultiTenant
{
    public PermitTrackId Id { get; private set; }
    public PermitTrackAction PermitTrackAction { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string Description { get; private set; }
    public UserId EmployeeId { get; private set; }
    public Employee HandledBy { get; private set; }
    public PermitId PermitId { get; private set; }
    public Permit Permit { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; }

    private PermitTrack()
    {
    }
    private PermitTrack(PermitTrackAction permitTrackAction, string description, PermitId permitId, UserId HandledBy)
    {
        PermitTrackAction = permitTrackAction;
        Description = description;
        PermitId = permitId;
        EmployeeId = HandledBy;
    }

    public static PermitTrack Create(PermitTrackAction permitTrackAction, string description, PermitId permitId, UserId HandledBy)
    {
        return new(permitTrackAction, description, permitId, HandledBy)
        {
            Id = new PermitTrackId(Guid.NewGuid())
        };
    }

}

public record PermitTrackId(Guid Id);
