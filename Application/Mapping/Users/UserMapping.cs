using Application.Users.Dtos;
using Domain.CatalogDb.SharedUsers;
using Domain.TenantDomain.Users;

namespace Application.Users.Mapping;

public partial class UserMapping : Profile
{
    public UserMapping()
    {
        MapGetUser();
    }
}

public partial class UserMapping
{
    public void MapGetUser()
    {
        CreateMap<SharedUser, GetUserDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()));

        CreateMap<User, GetUserDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()));

        CreateMap<Employee, GetUserDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.FirstName + " " + x.LastName));
    }
}
