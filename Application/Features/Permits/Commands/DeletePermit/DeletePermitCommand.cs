namespace Application.Features.Permits.Commands.DeletePermit;

public sealed record DeletePermitCommand(string PermitId) : IRequest<Response<string>>, IValidatorRequest;
