using Domain.Permits.Enums;

namespace Application.Dtos.Permits;

public class PermitDto
{
    public string PermitId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public PermitStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string VisitorName { get; set; }
    public string BuldingName { get; set; }
    public int FloorNumber { get; set; }
}
