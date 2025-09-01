using Domain.TenantDomain.Permits;
using Domain.TenantDomain.PermitUpdateRequests;
using Domain.TenantDomain.Users.Enums;

namespace Domain.TenantDomain.Users;

public class Requester(string firstName, string lastName, string email, string hashedPassword
                                   , string phoneNumber, PersonGender gender)

    : Employee(firstName, lastName, email, hashedPassword, phoneNumber, gender)
{
    private readonly List<Permit> _permits = [];
    public IReadOnlyCollection<Permit> Permits => _permits.AsReadOnly();

    private readonly List<PermitUpdateRequest> _permitUpdateRequests = [];
    public IReadOnlyCollection<PermitUpdateRequest> PermitUpdateRequest => _permitUpdateRequests.AsReadOnly();

}
