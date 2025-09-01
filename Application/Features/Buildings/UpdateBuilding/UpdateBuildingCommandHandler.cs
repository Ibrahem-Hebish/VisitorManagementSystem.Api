using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Buildings.Repositories;

namespace Application.Features.Buildings.UpdateBuilding;

public sealed class UpdateBuildingCommandHandler(
    IBuildingQueryRepository buildingQueryRepository,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<UpdateBuildingCommand, Response<string>>
{
    public async Task<Response<string>> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var building = await buildingQueryRepository.GetByIdAsync(new BuildingId(new Guid(request.Id)));

            if (building is null)
                return NotFound<string>();

            building.UpdateName(request.Name);

            building.UpdateFloorNumbers(request.FloorsNumber);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Success<string>($"Building with id {request.Id} updated successfully.");

        }
        catch (Exception ex)
        {
            // Log

            return InternalServerError<string>();
        }
    }
}
