using Application.Dtos.EntryLogs;
using Domain.TenantDomain.EntryLogs;

namespace Application.Mapping.EntryLogs;

public partial class Mapping : Profile
{
    public Mapping()
    {
        MapEntryLogDto();
    }
}

public partial class Mapping
{
    public void MapEntryLogDto()
    {
        CreateMap<Entrylog, EntryLogDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(pt => pt.Id.Value.ToString()))
            .ForMember(dest => dest.SecurityName, src => src.MapFrom(el => el.Employee.FirstName + " " + el.Employee.LastName));
    }
}
