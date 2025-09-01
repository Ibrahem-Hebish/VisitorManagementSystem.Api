using Application.Dtos.PermitUpdateRequests;

namespace Application.Features.PermitUpdateRequests.GetById;

public sealed record GetPermitUpdateRequestByIdQuery(string Id) : IRequest<Response<GetPermitUpdateRequestDto>>, IValidatorRequest;
