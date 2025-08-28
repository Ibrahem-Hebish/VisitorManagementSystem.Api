using Application.Dtos.Attachments;
using Application.Dtos.Belongings;
using Application.Dtos.Visitors;
using Domain.Permits.Enums;

namespace Application.Dtos.Permits;

public record PermitDetailsDto
{
    public string PermitId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public PermitStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string BuldingName { get; set; }
    public int FloorNumber { get; set; }
    public IReadOnlyList<BelongingDto> BelongingDtos { get; set; }
    public IReadOnlyList<AttachmentDto> Attachments { get; set; }
    public VisitorDto Visitor { get; set; }
}



public record CreatePermit
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public string BuldingId { get; set; }
    public int FloorNumber { get; set; }
    public List<CreateBelonging>? BelongingDtos { get; set; }
    public List<CreateAttachment>? Attachments { get; set; }
    public string VisitorId { get; set; }
}
