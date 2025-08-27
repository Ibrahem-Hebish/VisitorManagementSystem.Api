namespace Domain.Permits.DomainEvents;

public record PermitExpiredDomainEvent(PermitId PermitId, string VisitorEmail) : DomainEvent { }
