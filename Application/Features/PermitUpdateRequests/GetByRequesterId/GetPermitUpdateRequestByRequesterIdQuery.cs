using Application.Dtos.PermitUpdateRequests;

namespace Application.Features.PermitUpdateRequests.GetByRequesterId;


public sealed record GetPermitUpdateRequestByRequesterIdQuery(string ReqesterId) : IRequest<Response<List<GetPermitUpdateRequestDto>>>, IValidatorRequest;
