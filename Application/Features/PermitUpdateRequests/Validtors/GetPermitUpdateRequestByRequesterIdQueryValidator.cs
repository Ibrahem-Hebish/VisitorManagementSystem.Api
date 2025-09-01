using Application.Features.PermitUpdateRequests.GetByRequesterId;

namespace Application.Features.PermitUpdateRequests.Validtors;

public sealed class GetPermitUpdateRequestByRequesterIdQueryValidator
    : AbstractValidator<GetPermitUpdateRequestByRequesterIdQuery>
{
    public GetPermitUpdateRequestByRequesterIdQueryValidator()
    {
        RuleFor(x => x.ReqesterId)
            .NotEmpty().WithMessage("RequesterID is required.")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("RequesterID must be a valid GUID.");
    }
}