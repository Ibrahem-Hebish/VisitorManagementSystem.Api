using Application.Dtos.PermitUpdateRequests;

namespace Application.Features.PermitUpdateRequests.GetAll;

public sealed record GetAllPermitUpdateRequestsQuery : IRequest<Response<List<GetPermitUpdateRequestDto>>>;
