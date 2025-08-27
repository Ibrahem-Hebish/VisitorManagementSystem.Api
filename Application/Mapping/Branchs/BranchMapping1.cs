namespace Application.Branchs.Dtos;

public partial class BranchMapping : Profile
{
    public BranchMapping()
    {
        MapSharedBranch();
        MapBranchDetails();
    }
}
