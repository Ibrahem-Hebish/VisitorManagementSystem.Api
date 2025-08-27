using Application.Users.Dtos;
using Domain.Branches.ObjectValues;

namespace Application.Dtos.Branch;

public record BranchDetails
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public BranchAddress Address { get; set; }
    public string Email { get; set; }
    public GetUserDto? Admin { get; set; }

}