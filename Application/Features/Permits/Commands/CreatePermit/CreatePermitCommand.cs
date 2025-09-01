using Application.Dtos.Attachments;
using Application.Dtos.Belongings;
using Application.Dtos.Visitors;

namespace Application.Features.Permits.Commands.CreatePermit;

public sealed record CreatePermitCommand : IRequest<Response<string>>, IValidatorRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public string BuildingId { get; set; }
    public int FloorNumber { get; set; }
    public List<CreateBelonging>? BelongingDtos { get; set; }
    public List<CreateAttachment>? Attachments { get; set; }
    public List<CreateVisitor> Visitors { get; set; }
}
