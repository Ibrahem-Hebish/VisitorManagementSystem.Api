using Domain.TenantDomain.PermitTracks.Enums;

namespace Application.Dtos.PermitTracks;

public record PermitTrackDto(
    string Id,
    PermitTrackAction PermitTrackAction,
    DateTime CreatedAt,
    string Description,
    string EmpolyeeName
    );
