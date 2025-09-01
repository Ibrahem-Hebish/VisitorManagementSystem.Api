using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits.ObjectValues;

namespace Domain.TenantDomain.EntryLogs.DomainEvents;

public sealed record PermitTimeHasStartedDomainEvent(PermitId PermitId) : DomainEvent;
