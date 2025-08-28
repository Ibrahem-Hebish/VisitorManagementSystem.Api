using Domain.Users.Enums;

namespace Application.Dtos.Visitors;

public record VisitorDto
{
    public string VisitorId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string NationalId { get; set; } = "";
    public PersonGender Gender { get; set; }
}
