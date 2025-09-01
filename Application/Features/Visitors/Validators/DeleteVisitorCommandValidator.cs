using Application.Features.Visitors.DeleteVisitor;

namespace Application.Features.Visitors.Validators;

public class DeleteVisitorCommandValidator : AbstractValidator<DeleteVisitorCommand>
{
    public DeleteVisitorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Id must be a valid GUID.");
    }
}
