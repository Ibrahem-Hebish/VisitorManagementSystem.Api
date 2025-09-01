using Application.Dtos.Buildings;
using Domain.TenantDomain.Buildings;

namespace Application.Mapping.Buildings;

public partial class BuildingMappings : Profile
{
    public BuildingMappings()
    {
        MapBuildingDto();
    }
}

public partial class BuildingMappings
{
    public void MapBuildingDto()
    {
        CreateMap<Building, GetBuildingDto>();
    }
}
