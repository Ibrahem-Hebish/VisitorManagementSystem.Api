using Application.Dtos.Permits;
using Domain.Permits;

namespace Application.Mapping.Permits;

public partial class PermitMapping : Profile
{
    public PermitMapping()
    {
        MapPermitDto();
        MapPermitDetails();
    }
}

public partial class PermitMapping
{
    public void MapPermitDto()
    {
        CreateMap<Permit, PermitDto>()
            .ForMember(dest => dest.PermitId, src => src.MapFrom(p => p.PermitId.Id.ToString()))
            .ForMember(dest => dest.BuldingName, src => src.MapFrom(p => p.Building.ToString()));

    }
}

public partial class PermitMapping
{
    public void MapPermitDetails()
    {
        CreateMap<Permit, PermitDetailsDto>()
            .ForMember(dest => dest.PermitId, src => src.MapFrom(p => p.PermitId.Id.ToString()))
            .ForMember(dest => dest.BuldingName, src => src.MapFrom(p => p.Building.ToString()));
    }
}