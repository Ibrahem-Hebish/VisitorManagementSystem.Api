namespace Application.Features.Visitors.DeleteVisitor;

public sealed record DeleteVisitorCommand(string Id) : IRequest<Response<string>>, IValidatorRequest;
