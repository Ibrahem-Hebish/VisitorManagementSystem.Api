namespace Domain.Users;

public class Requester(string firstName, string lastName, string email, string hashedPassword
                                   , string phoneNumber)

    : Employee(firstName, lastName, email, hashedPassword, phoneNumber)
{
    private readonly List<Permit> _permits = [];
    public IReadOnlyCollection<Permit> Permits => _permits.AsReadOnly();

    private readonly List<PermitUpdateRequest> _permitUpdateRequests = [];
    public IReadOnlyCollection<PermitUpdateRequest> PermitUpdateRequest => _permitUpdateRequests.AsReadOnly();

}
