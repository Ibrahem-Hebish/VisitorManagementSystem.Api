using Application.Features.EntryLogs.CreateEntryLog;

namespace Application.Features.EntryLogs.Validators;

public sealed class CreateEntryLogCommandValidator
    : AbstractValidator<CreateEntryLogCommand>
{
    public CreateEntryLogCommandValidator()
    {
        RuleFor(x => x.PermitId)
            .NotEmpty().WithMessage("PermitId is required.")
            .Must(BeValidGuid).WithMessage("PermitId must be a valid GUID.");
    }

    private bool BeValidGuid(string permitId)
    {
        return Guid.TryParse(permitId, out _);
    }
}
