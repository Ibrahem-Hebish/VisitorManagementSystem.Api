using Application.Dtos.Visitors;

namespace Application.Features.Visitors.CreateNewVisitor;

public sealed record CreateVisitorCommand : CreateVisitor, IRequest<Response<string>>, IValidatorRequest;



