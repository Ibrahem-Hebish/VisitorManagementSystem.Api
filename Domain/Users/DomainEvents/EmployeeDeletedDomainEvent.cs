namespace Domain.Users.DomainEvents;

public sealed record EmployeeDeletedDomainEvent(string UserId) : DomainEvent;
