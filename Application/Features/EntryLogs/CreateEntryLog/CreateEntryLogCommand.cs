namespace Application.Features.EntryLogs.CreateEntryLog;

public sealed record CreateEntryLogCommand(string PermitId) : IRequest<Response<string>>,IValidatorRequest;
