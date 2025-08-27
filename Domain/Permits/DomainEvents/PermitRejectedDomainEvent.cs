namespace Domain.Permits.DomainEvents;

public record PermitRejectedDomainEvent(PermitId PermitId, string VisitorEmail) : DomainEvent { }
