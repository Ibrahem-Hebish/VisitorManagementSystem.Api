using Application.Dtos.Buildings;
using Domain.TenantDomain.Buildings.ObjectValues;
using Domain.TenantDomain.Buildings.Repositories;

namespace Application.Features.Buildings.GetBuildingById;

public sealed class GetBuildingByIdQueryHandler(
    IBuildingQueryRepository buildingQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetBuildingByIdQuery, Response<GetBuildingDto>>
{
    public async Task<Response<GetBuildingDto>> Handle(GetBuildingByIdQuery request, CancellationToken cancellationToken)
    {
        var building = await buildingQueryRepository.GetByIdAsync(new BuildingId(new Guid(request.Id)));

        if (building is null)
            return NotFound<GetBuildingDto>();

        var dto = mapper.Map<GetBuildingDto>(building);

        return Success(dto);
    }
}
