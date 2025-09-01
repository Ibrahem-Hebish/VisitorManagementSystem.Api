using Application.Features.PermitUpdateRequests.Create;

namespace Application.Features.PermitUpdateRequests.Validtors;

public sealed class CreatePermitUpdateRequestCommandValidator
    : AbstractValidator<CreatePermitUpdateRequestCommand>
{
    public CreatePermitUpdateRequestCommandValidator()
    {
        RuleFor(x => x.PermitId)
            .NotEmpty().WithMessage("PermitId is required.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("PermitId must be a valid GUID.");

        RuleFor(x => x.Action)
            .IsInEnum()
            .WithMessage("Invalid action.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}
