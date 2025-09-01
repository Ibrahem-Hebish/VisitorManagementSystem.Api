using Application.Features.Buildings.DeleteBuilding;

namespace Application.Features.Buildings.Validators;

public sealed class DeleteBuildingCommandValidator : AbstractValidator<DeleteBuildingCommand>
{
    public DeleteBuildingCommandValidator()
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
