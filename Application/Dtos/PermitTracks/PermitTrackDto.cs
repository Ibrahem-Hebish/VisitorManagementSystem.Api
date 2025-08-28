using Domain.PermitTracks.Enums;

namespace Application.Dtos.PermitTracks;

public record PermitTrackDto
{
    public string Id { get; set; }
    public PermitTrackAction PermitTrackAction { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public string EmpolyeeName { get; set; }
}
