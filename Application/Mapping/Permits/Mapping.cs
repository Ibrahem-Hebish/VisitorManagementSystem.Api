using Application.Dtos.Permits;
using Domain.Permits;

namespace Application.Mapping.Permits;

public partial class Mapping : Profile
{
    public Mapping()
    {
        MapDto();
    }
}

public partial class Mapping
{
    public void MapDto()
    {
        CreateMap<Permit, PermitDto>()
            .ForMember(dest => dest.PermitId, src => src.MapFrom(p => p.PermitId.Id.ToString()))
            .ForMember(dest => dest.BuldingName, src => src.MapFrom(p => p.Building.ToString()));

    }
}