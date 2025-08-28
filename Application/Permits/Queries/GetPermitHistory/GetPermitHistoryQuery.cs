using Application.Dtos.PermitTracks;

namespace Application.Permits.Queries.GetPermitHistory;

public sealed record GetPermitHistoryQuery(string PermitId) : IRequest<Response<IReadOnlyList<PermitTrackDto>>>;
