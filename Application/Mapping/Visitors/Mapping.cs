using Application.Dtos.Visitors;
using Domain.TenantDomain.Visitors;

namespace Application.Mapping.Visitors;

public partial class VisitorMapping : Profile
{
    public VisitorMapping()
    {
        MapVisitorDto();
    }
}


public partial class VisitorMapping
{
    public void MapVisitorDto()
    {
        CreateMap<Visitor, VisitorDto>()
            .ForMember(dest => dest.VisitorId, src => src.MapFrom(v => v.Id.Value.ToString()));
    }
}