using Application.Features.Permits.Commands.CreatePermit;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Buildings.Repositories;

namespace Application.Features.Permits.Commands.Validators;
public class CreatePermitValidator : AbstractValidator<CreatePermitCommand>
{
    public CreatePermitValidator(
        IVisitorQueryRepository visitorRepository,
        IBuildingQueryRepository buildingRepository)
    {
        RuleFor(x => x.StartDate)
            .Must(startDate => startDate >= DateTime.UtcNow)
            .WithMessage("Start date must be today or later.");

        RuleFor(x => x)
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("End date must be greater than start date.");

        RuleFor(x => x.BuildingId)
            .NotEmpty().WithMessage("Building ID is required.")
            .MustAsync(async (buildingId, cancellation) =>
            {
                return await buildingRepository.ExsistsAsync(new BuildingId(new Guid(buildingId)));
            })
            .WithMessage("Building does not exist.");

        RuleFor(x => x)
            .MustAsync(async (model, cancellation) =>
            {
                if (model.FloorNumber <= 0) return false;

                var floorsCount = await buildingRepository.GetFloorsCountAsync(new BuildingId(new Guid(model.BuildingId)));
                return model.FloorNumber <= floorsCount;
            })
            .WithMessage("Floor number is invalid for the selected building.");

        RuleFor(x => x.Visitors.Count)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Permit Must attached to one visitor at least.");

    }
}

