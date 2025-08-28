namespace Application.Permits.Commands.Validators;
public class CreatePermitValidator : AbstractValidator<CreatePermitCommand>
{
    public CreatePermitValidator(
        IVisitorQueryRepository visitorRepository,
        IBuildingQueryRepository buildingRepository)
    {
        // 1. StartDate must be >= DateTime.Now
        RuleFor(x => x.StartDate)
            .Must(startDate => startDate >= DateTime.UtcNow)
            .WithMessage("Start date must be today or later.");

        // 2. EndDate must be greater than StartDate
        RuleFor(x => x)
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("End date must be greater than start date.");

        // 3. Building must exist
        RuleFor(x => x.BuildingId)
            .NotEmpty().WithMessage("Building ID is required.")
            .MustAsync(async (buildingId, cancellation) =>
            {
                return await buildingRepository.ExsistsAsync(new BuildingId(new Guid(buildingId)));
            })
            .WithMessage("Building does not exist.");

        // 4. Floor number must be valid according to building
        RuleFor(x => x)
            .MustAsync(async (model, cancellation) =>
            {
                if (model.FloorNumber <= 0) return false;

                var floorsCount = await buildingRepository.GetFloorsCountAsync(new BuildingId(new Guid(model.BuildingId)));
                return model.FloorNumber <= floorsCount;
            })
            .WithMessage("Floor number is invalid for the selected building.");

        // 5. Visitor must exist
        RuleFor(x => x.VisitorId)
            .NotEmpty().WithMessage("Visitor is required.")
            .MustAsync(async (visitorId, cancellation) =>
            {
                return await visitorRepository.ExsistsAsync(new VisitorId(new Guid(visitorId)));
            })
            .WithMessage("Visitor does not exist.");
    }
}

