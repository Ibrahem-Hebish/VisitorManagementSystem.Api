using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetPermitDetails;

public sealed record GetPermitDetails(string PermitId) : IRequest<Response<PermitDto>>;