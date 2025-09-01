using Domain.TenantDomain.Users.Enums;

namespace Application.Features.Employees.CreateEmployee;

public record CreateEmployeeCommand : IRequest<Response<string>>, IValidatorRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string PhoneNumber { get; set; }
    public PersonGender Gender { get; set; }
    public EmployeePosition Position { get; set; }
}
