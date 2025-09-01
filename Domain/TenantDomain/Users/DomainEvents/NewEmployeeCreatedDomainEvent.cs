using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Users.Enums;

namespace Domain.TenantDomain.Users.DomainEvents;

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
