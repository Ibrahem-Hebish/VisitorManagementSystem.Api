using Domain.TenantDomain.Common;
using Domain.TenantDomain.Tenants;

namespace Domain.TenantDomain.Users.DomainEvents;

public sealed record TenantAdminCreatedDomainEvent(string UserId, Tenant Tenant) : DomainEvent;


