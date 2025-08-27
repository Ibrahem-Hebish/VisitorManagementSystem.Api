using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetPermitsCreatedByRequester;

public sealed record GetPermitsCreatedByRequesterQuery(string Id) : IRequest<Response<List<PermitDto>>>;

