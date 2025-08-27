using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetPermitsHandedByManager;

public sealed record GetPermitsHandledByManagerQuery(string Id) : IRequest<Response<List<PermitDto>>>;
