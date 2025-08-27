using Application.Dtos.Branch;

namespace Application.Dtos.Tenant;

public record TenantDetailsDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<BranchDto> Branches { get; set; }
}

