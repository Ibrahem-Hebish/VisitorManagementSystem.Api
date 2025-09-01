using Application.Features.Permits.Commands.ExtendPermit;

namespace Application.Features.Permits.Commands.Validators;

public sealed class ExtendPermitCommandValidator : AbstractValidator<ExtendPermitCommand>
{
    public ExtendPermitCommandValidator()
    {
        RuleFor(x => x.PermitId)
            .NotEmpty().WithMessage("PermitId is required.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("PermitId must be a valid GUID.");

        RuleFor(x => x.NewEndDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("New end date must be in the future.");
    }
}
