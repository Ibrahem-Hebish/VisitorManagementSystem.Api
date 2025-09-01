using Application.Dtos.Permits;

namespace Application.Features.Permits.Queries.GetPermitDetails;

public sealed record GetPermitDetails(string PermitId) : IRequest<Response<PermitDetailsDto>>;
