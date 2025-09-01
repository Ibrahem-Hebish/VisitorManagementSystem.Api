using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Users.Enums;
using Domain.TenantDomain.VisitorPermits;
using Domain.TenantDomain.Visitors.ObjectValues;

namespace Domain.TenantDomain.Visitors;



public class Visitor : IMultiTenant
{
    public VisitorId Id { get; private set; }
    public string FirstName { get; private set; } = "";
    public string LastName { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string PhoneNumber { get; private set; } = "";
    public string NationalId { get; private set; } = "";
    public PersonGender Gender { get; private set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; } = null!;
    private readonly List<Permit> _permits = [];
    public IReadOnlyCollection<Permit> Permits => _permits.AsReadOnly();
    private readonly List<VisitorPermit> _vistorPermits = [];
    public IReadOnlyCollection<VisitorPermit> VisitorPermits => _vistorPermits.AsReadOnly();

    private Visitor() { }
    private Visitor(string firstName, string lastName, string email,
        string phoneNumber, string nationalId, PersonGender gender)
    {
        Id = new VisitorId(Guid.NewGuid());
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        NationalId = nationalId;
        Gender = gender;
    }

    public static Visitor Create(string firstName, string lastName, string email,
        string phoneNumber, string nationalId, PersonGender gender)
        => new(firstName, lastName, email, phoneNumber, nationalId, gender);

    public void UpdateFirstName(string firstName) => FirstName = firstName;
    public void UpdateLastName(string lastName) => LastName = lastName;
    public void UpdateEmail(string email) => Email = email;
    public void UpdatePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;
    public void UpdateNationalId(string nationalId) => NationalId = nationalId;
    public void UpdateDetails(string firstName, string lastName, string nationalId, string phoneNumber)
    {
        PhoneNumber = phoneNumber;

        FirstName = firstName;

        LastName = lastName;

        NationalId = nationalId;
    }

}
