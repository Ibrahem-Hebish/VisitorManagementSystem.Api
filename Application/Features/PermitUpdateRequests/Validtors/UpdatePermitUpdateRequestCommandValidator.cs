using Application.Features.PermitUpdateRequests.Update;

namespace Application.Features.PermitUpdateRequests.Validtors;

public sealed class UpdatePermitUpdateRequestCommandValidator
    : AbstractValidator<UpdatePermitUpdateRequestCommand>
{
    public UpdatePermitUpdateRequestCommandValidator()
    {
        RuleFor(x => x.Id)
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
