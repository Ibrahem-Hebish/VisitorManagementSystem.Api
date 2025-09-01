using Application.Dtos.PermitUpdateRequests;

namespace Application.Features.PermitUpdateRequests.GetLatest;

public sealed record GetLatestPermitUpdateRequests : IRequest<Response<List<GetPermitUpdateRequestDto>>>;
