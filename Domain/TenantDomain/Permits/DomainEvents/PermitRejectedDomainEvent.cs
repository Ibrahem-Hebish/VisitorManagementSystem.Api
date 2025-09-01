using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits.ObjectValues;

namespace Domain.TenantDomain.Permits.DomainEvents;

public record PermitRejectedDomainEvent(PermitId PermitId, List<string> Emails) : DomainEvent { }
