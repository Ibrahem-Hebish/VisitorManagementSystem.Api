using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits.ObjectValues;

namespace Domain.TenantDomain.Permits.DomainEvents;

public record PermitApprovedDomainEvent(PermitId PermitId, List<string> Emails) : DomainEvent { }
