using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Buildings.Repositories;

namespace Application.Features.Buildings.CreateBuilding;

public sealed class CreateBuildingCommandHandler(
    IBuildingCommandRepository buildingCommandRepository,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<CreateBuildingCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var building = Building.Create(request.Name, request.FloorsNumber);

            await buildingCommandRepository.AddAsync(building);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Created<string>();

        }
        catch (Exception ex)
        {
            // Log

            return InternalServerError<string>();
        }
    }
}
