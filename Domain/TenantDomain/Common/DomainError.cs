namespace Domain.TenantDomain.Common;

public class DomainError(string message): Exception(message)
{

}
