using Application.Dtos.Attachments;
using Application.Dtos.Belongings;
using Application.Dtos.Visitors;
using Domain.TenantDomain.Permits.Enums;

namespace Application.Dtos.Permits;

public record PermitDetailsDto(
    string PermitId,
    DateTime StartDate,
    DateTime EndDate,
    string Reason,
    PermitStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string BuldingName,
    int FloorNumber,
    IReadOnlyList<BelongingDto> BelongingDtos,
    IReadOnlyList<AttachmentDto> Attachments,
    List<VisitorDto> Visitors
    );
