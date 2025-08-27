namespace Domain.Permits.DomainEvents;

public record NewPermitCreatedDomainEvent(PermitId PermitId, string VisitorEmail) : DomainEvent { }
