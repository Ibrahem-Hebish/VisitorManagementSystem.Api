using Application.Dtos.Permits;

namespace Application.Features.Permits.Queries.GetPermitsCreatedByRequester;

public sealed record GetPermitsCreatedByRequesterQuery(string Id) : IRequest<Response<List<PermitDto>>>;

