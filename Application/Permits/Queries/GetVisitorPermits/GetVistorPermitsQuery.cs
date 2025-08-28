using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetVisitorPermits;

public sealed record GetVistorPermitsQuery(string Id) : IRequest<Response<List<PermitDto>>>;

