using Application.Dtos.Branch;
using Domain.Branches;
using Domain.SharedTenantMetadataEntities.Branches;

namespace Application.Branchs.Dtos;

public partial class BranchMapping
{
    public void MapSharedBranch()
    {
        CreateMap<SharedBranch, BranchDto>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Guid.ToString()));
    }
}

public partial class BranchMapping
{
    public void MapBranchDetails()
    {
        CreateMap<Branch, BranchDetails>()
            .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id.Guid.ToString()))
             .ForMember(dest => dest.Admin, opt => opt.MapFrom(
                                                     src => src.Employees
                                                               .FirstOrDefault(e => e.Role != null && e.Role.Name == "BranchAdmin")));
    }
}