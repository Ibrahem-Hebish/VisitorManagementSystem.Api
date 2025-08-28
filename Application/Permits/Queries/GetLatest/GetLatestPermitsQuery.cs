using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetLatest;

public sealed record GetLatestPermitsQuery(int? Count) : IRequest<Response<List<PermitDto>>>;


