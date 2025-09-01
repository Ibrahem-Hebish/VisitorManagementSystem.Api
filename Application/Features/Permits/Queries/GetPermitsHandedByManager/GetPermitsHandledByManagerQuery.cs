using Application.Dtos.Permits;

namespace Application.Features.Permits.Queries.GetPermitsHandedByManager;

public sealed record GetPermitsHandledByManagerQuery(string Id) : IRequest<Response<List<PermitDto>>>;
