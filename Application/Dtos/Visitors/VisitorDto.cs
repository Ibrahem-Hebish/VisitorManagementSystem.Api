using Domain.TenantDomain.Users.Enums;

namespace Application.Dtos.Visitors;

public record VisitorDto(
    string VisitorId,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string NationalId,
    PersonGender Gender);
