using Application.Dtos.Visitors;

namespace Application.Features.Visitors.GetVisitorById;

public sealed record GetVisitorByIdQuery(string VisitorId) : IRequest<Response<VisitorDto>>, IValidatorRequest;
