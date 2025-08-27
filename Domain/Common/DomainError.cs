namespace Domain.Common;

public class DomainError(string message): Exception(message)
{

}
