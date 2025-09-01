using Application.Features.Buildings.UpdateBuilding;

namespace Application.Features.Buildings.Validators;

public sealed class UpdateBuildingCommandValidator : AbstractValidator<UpdateBuildingCommand>
{
    public UpdateBuildingCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Building Id is required.")
            .Must(BeAValidGuid).WithMessage("Building Id must be a valid GUID.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Building name is required.")
            .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");

        RuleFor(x => x.FloorsNumber)
            .GreaterThan(0).WithMessage("Floors number must be greater than 0.");
    }

    private bool BeAValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}