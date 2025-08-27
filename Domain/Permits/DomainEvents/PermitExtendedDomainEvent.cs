namespace Domain.Permits.DomainEvents;

public record PermitExtendedDomainEvent(PermitId PermitId, string VisitorEmail) : DomainEvent { }
