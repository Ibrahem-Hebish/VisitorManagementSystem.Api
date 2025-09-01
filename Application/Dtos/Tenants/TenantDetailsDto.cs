using Application.Dtos.Branch;
using Application.Users.Dtos;

namespace Application.Dtos.Tenant;

public record TenantDetailsDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public GetUserDto Manager { get; set; }
    public List<BranchDto> Branches { get; set; }
}
