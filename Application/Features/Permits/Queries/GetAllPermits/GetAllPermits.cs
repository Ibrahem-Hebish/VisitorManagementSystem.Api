using Application.Dtos.Permits;

namespace Application.Features.Permits.Queries.GetAllPermits;

public sealed record GetAllPermitsQuery() : IRequest<Response<List<PermitDto>>>;



