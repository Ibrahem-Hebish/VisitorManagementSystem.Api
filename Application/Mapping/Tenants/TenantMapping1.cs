using Application.Dtos.Tenant;
using Domain.CatalogDb.Tenants;

namespace Application.Tenants;

public partial class TenantMapping
{
    public void Map()
    {
        CreateMap<SharedTenant, TenantDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()));
    }
}

public partial class TenantMapping
{
    public void MapDetails()
    {
        CreateMap<SharedTenant, TenantDetailsDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()));
    }
}