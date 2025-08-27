namespace Domain.Permits.DomainEvents;

public record PermitCanceledDomainEvent(PermitId PermitId, string VisitorEmail) : DomainEvent { }
