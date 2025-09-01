using Application.Features.Permits.Commands.DeletePermit;

namespace Application.Features.Permits.Commands.Validators;

public sealed class DeletePermitCommandValidator : AbstractValidator<DeletePermitCommand>
{
    public DeletePermitCommandValidator()
    {
        RuleFor(x => x.PermitId)
            .NotEmpty().WithMessage("PermitId is required.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("PermitId must be a valid GUID.");

    }
}