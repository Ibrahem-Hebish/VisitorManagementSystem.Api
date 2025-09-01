using Domain.TenantDomain.PermitUpdateRequests.Enums;

namespace Application.Dtos.PermitUpdateRequests;

public record GetPermitUpdateRequestDto
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public PermitUpdateAction PermitUpdateAction { get; set; }
    public string PermitId { get; set; }
    public string UserId { get; set; }
}
