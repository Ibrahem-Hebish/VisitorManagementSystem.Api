using Application.Permits.Commands.ApprovePermit;

namespace Application.Permits.Commands.Validators;

public sealed class ApprovePermitCommandValidator : AbstractValidator<ApprovePermitCommand>
{
    public ApprovePermitCommandValidator()
    {
        RuleFor(x => x.PermitId)
            .NotEmpty().WithMessage("PermitId is required.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("PermitId must be a valid GUID.");
    }
}

