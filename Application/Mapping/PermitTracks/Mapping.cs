using Application.Dtos.PermitTracks;
using Domain.PermitTracks;

namespace Application.Mapping.PermitTracks;

public partial class Mapping : Profile
{
    public Mapping()
    {
        MapPermitTrackDto();
    }
}

public partial class Mapping
{
    public void MapPermitTrackDto()
    {
        CreateMap<PermitTrack, PermitTrackDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(pt => pt.Id.Id.ToString()))
            .ForMember(dest => dest.EmpolyeeName, src => src.MapFrom(pt => pt.HandledBy.FirstName + " " + pt.HandledBy.LastName));
    }
}