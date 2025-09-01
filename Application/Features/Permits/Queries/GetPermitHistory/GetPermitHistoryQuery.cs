using Application.Dtos.PermitTracks;

namespace Application.Features.Permits.Queries.GetPermitHistory;

public sealed record GetPermitHistoryQuery(string PermitId) : IRequest<Response<IReadOnlyList<PermitTrackDto>>>;
