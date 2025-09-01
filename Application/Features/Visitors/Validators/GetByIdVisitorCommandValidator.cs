using Application.Features.Visitors.GetVisitorById;

namespace Application.Features.Visitors.Validators;

public class GetByIdVisitorCommandValidator : AbstractValidator<GetVisitorByIdQuery>
{
    public GetByIdVisitorCommandValidator()
    {
        RuleFor(x => x.VisitorId)
            .NotEmpty().WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id, out _)).WithMessage("Id must be a valid GUID.");
    }
}
