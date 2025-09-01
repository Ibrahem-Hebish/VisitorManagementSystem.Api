using Application.Dtos.Visitors;
using Domain.TenantDomain.Permits.Enums;

namespace Application.Dtos.Permits;

public record PermitDto(
    string PermitId,
    DateTime StartDate,
    DateTime EndDate,
    string Reason,
    PermitStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string BuldingName,
    int FloorNumber,
    List<VisitorDto> Visitors);
