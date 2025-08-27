using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetPermitById;

public sealed record GetPermitByIdQuery(string PermitId) : IRequest<Response<PermitDto>>;
