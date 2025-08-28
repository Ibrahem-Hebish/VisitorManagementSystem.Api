using Application.Dtos.Belongings;
using Domain.Belongings;

namespace Application.Mapping.Belongings;

public partial class BelongingMapping : Profile
{
    public BelongingMapping()
    {
        MapBelongingDto();
    }
}

public partial class BelongingMapping
{
    public void MapBelongingDto()
    {
        CreateMap<Belonging, BelongingDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(b => b.Id.Id.ToString()))
            .ForMember(dest => dest.PlateNumber, opt =>
            {
                opt.Condition(src => src is Car);
                opt.MapFrom(src => (src as Car)!.PlateNumber);
            })
            .ForMember(dest => dest.Color, opt =>
            {
                opt.Condition(src => src is Car);
                opt.MapFrom(src => (src as Car)!.Color);
            });
    }
}
