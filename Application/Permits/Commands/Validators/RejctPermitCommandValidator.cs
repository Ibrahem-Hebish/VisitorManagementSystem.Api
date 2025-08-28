using Application.Permits.Commands.RejectPermit;

namespace Application.Permits.Commands.Validators;

public sealed class RejctPermitCommandValidator : AbstractValidator<RejectPermitCommand>
{
    public RejctPermitCommandValidator()
    {
        RuleFor(x => x.PermitId)
            .NotEmpty().WithMessage("PermitId is required.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("PermitId must be a valid GUID.");
    }
}