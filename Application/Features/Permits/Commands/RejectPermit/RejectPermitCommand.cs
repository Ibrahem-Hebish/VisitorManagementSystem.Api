namespace Application.Features.Permits.Commands.RejectPermit;

public sealed record RejectPermitCommand(string PermitId) : IRequest<Response<string>>, IValidatorRequest;
