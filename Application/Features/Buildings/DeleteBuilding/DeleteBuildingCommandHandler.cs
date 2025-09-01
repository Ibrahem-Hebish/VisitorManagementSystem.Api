using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Buildings.Repositories;

namespace Application.Features.Buildings.DeleteBuilding;

public sealed class DeleteBuildingCommandHandler(
    IBuildingQueryRepository buildingQueryRepository,
    IBuildingCommandRepository buildingCommandRepository,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<DeleteBuildingCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync();

            var buildingId = new BuildingId(new Guid(request.Id));

            var building = await buildingQueryRepository.GetByIdAsync(buildingId);

            if (building is null)
                return NotFound<string>();

            await buildingCommandRepository.SetPermitsBuildingIdToNull(buildingId);

            buildingCommandRepository.Delete(building);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync();

            return Deleted<string>();

        }
        catch (Exception ex)
        {
            // Logs

            await unitOfWork.RollbackTransactionAsync();

            return InternalServerError<string>();
        }
    }
}
