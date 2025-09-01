using Application.Dtos.Branch;
using Domain.CatalogDb.Branches;
using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Roles.Enums;

namespace Application.Branchs.Dtos;

public partial class BranchMapping
{
    public void MapSharedBranch()
    {
        CreateMap<SharedBranch, BranchDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()));
    }
}

public partial class BranchMapping
{
    public void MapBranchDetails()
    {
        CreateMap<Branch, BranchDetails>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Value.ToString()))
             .ForMember(dest => dest.Admin, opt => opt.MapFrom(
                                                     src => src.Employees
                                                     .FirstOrDefault(e => e.Role != null && e.Role.Name == Roles.BranchAdmin.ToString())));
    }
}