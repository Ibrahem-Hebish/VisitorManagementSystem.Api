using Application.Dtos.Buildings;
using Domain.TenantDomain.Buildings.Repositories;

namespace Application.Features.Buildings.GetAllBuildings;

public sealed class GetAllBuildingQueryHandler(
    IBuildingQueryRepository buildingQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllBuildingQuery, Response<List<GetBuildingDto>>>
{
    public async Task<Response<List<GetBuildingDto>>> Handle(GetAllBuildingQuery request, CancellationToken cancellationToken)
    {
        var buildings = await buildingQueryRepository.GetAllAsync();

        if (buildings is null || buildings.Count == 0)
            return NotFound<List<GetBuildingDto>>();

        var dtos = mapper.Map<List<GetBuildingDto>>(buildings);

        return Success(dtos);
    }
}
