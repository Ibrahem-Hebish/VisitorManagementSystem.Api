using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetAllPermits;

public sealed record GetAllPermitsQuery() : IRequest<Response<List<PermitDto>>>;



