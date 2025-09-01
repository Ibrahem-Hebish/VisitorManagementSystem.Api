using Application.Dtos.UserToken;
using AutoMapper;
using Domain.TenantDomain.Tokens;

namespace Application.UserTokens.Mapping;

public partial class UserTokenProfile : Profile
{
    public UserTokenProfile()
    {
        Map();

    }
}

public partial class UserTokenProfile
{
    public void Map()
    {
        CreateMap<UserToken, UserTokenDto>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.InUse));

        CreateMap<UserTokenDto, UserToken>()
            .ForMember(dest => dest.InUse, opt => opt.MapFrom(src => src.IsActive));
    }
}
