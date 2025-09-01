using Application.Features.Buildings.CreateBuilding;

namespace Application.Features.Buildings.Validators;

public class CreateBuildingValidator : AbstractValidator<CreateBuildingCommand>
{
    public CreateBuildingValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Building name is required.")
            .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");

        RuleFor(x => x.FloorsNumber)
            .GreaterThan(0).WithMessage("Floors number must be greater than 0.");
    }
}
