using Domain.TenantDomain.Common;

namespace Domain.TenantDomain.Users.DomainEvents;

public sealed record EmployeeDeletedDomainEvent(string UserId) : DomainEvent;


