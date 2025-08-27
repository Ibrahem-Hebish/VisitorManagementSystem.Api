using Application.Dtos.UserToken;

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
            .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId.Id.ToString()));
    }
}
