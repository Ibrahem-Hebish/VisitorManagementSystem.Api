namespace Domain.Permits.DomainEvents;

public record PermitApprovedDomainEvent(PermitId PermitId, string VisitorEmail) : DomainEvent { }
