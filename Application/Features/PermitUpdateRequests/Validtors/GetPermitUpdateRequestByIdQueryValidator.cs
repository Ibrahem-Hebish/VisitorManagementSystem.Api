using Application.Features.PermitUpdateRequests.GetById;

namespace Application.Features.PermitUpdateRequests.Validtors;

public sealed class GetPermitUpdateRequestByIdQueryValidator
    : AbstractValidator<GetPermitUpdateRequestByIdQuery>
{
    public GetPermitUpdateRequestByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("Id must be a valid GUID.");
    }
}
