using Domain.CatalogDb.Branches.ObjectValues;

namespace Application.Dtos.Branch;

public record BranchDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public SharedBranchAddress Address { get; set; }
}
