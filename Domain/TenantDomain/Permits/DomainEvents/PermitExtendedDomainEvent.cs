using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits.ObjectValues;

namespace Domain.TenantDomain.Permits.DomainEvents;

public record PermitExtendedDomainEvent(PermitId PermitId, DateTime EndDate, List<string> Emails) : DomainEvent { }
