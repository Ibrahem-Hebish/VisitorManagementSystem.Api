using Application.Features.Buildings.GetBuildingById;

namespace Application.Features.Buildings.Validators;

public sealed class GetBuildingByIdQueryValidator : AbstractValidator<GetBuildingByIdQuery>
{
    public GetBuildingByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Building Id is required.")
            .Must(BeAValidGuid).WithMessage("Building Id must be a valid GUID.");
    }

    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}