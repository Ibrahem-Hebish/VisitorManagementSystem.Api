using Application.Dtos.PermitUpdateRequests;
using Domain.TenantDomain.PermitUpdateRequests;

namespace Application.Mapping.PermitUpdateRequests;


public partial class PermitUpdateRequestMapping
{
    public void MapPermitUpdateRequestDto()
    {
        CreateMap<PermitUpdateRequest, GetPermitUpdateRequestDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()))
            .ForMember(dest => dest.PermitId, src => src.MapFrom(x => x.PermitId.Value.ToString()))
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.RequesterId.Value.ToString()));
    }
}