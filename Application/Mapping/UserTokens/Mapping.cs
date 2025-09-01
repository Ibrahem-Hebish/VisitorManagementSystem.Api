using Application.Dtos.UserToken;
using Domain.TenantDomain.Tokens;

namespace Application.Mapping.UserTokens;

public partial class Mapping : Profile
{
    public Mapping()
    {
        MapUserTokenDto();
    }
}


public partial class Mapping
{
    public void MapUserTokenDto()
    {
        CreateMap<UserToken, UserTokenDto>()
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId.Value.ToString()));
    }
}
