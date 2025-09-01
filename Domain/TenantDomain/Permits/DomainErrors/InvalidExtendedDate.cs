using Domain.TenantDomain.Common;

namespace Domain.TenantDomain.Permits.DomainErrors;

public class InvalidExtendedDate(string message) : DomainError(message)
{
}
