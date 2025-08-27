using Domain.Users.Enums;

namespace Domain.Users.DomainEvents;

public record NewEmployeeCreatedDomainEvent : DomainEvent
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public PersonGender Gender { get; init; }
    public BranchId BranchId { get; init; }
}
