using Domain.Users.Enums;
using Domain.VisitorPermits;

namespace Domain.Visitors;



public class Visitor
{
    public VisitorId VisitorId { get; private set; }
    public string FirstName { get; private set; } = "";
    public string LastName { get; private set; } = "";
    public string Email { get; private set; } = "";
    public string PhoneNumber { get; private set; } = "";
    public string NationalId { get; private set; } = "";
    public PersonGender Gender { get; private set; }
    public BranchId BranchId { get; private set; }
    public Branch Branch { get; private set; } = null!;
    private readonly List<Permit> _permits = [];
    public IReadOnlyCollection<Permit> Permits => _permits.AsReadOnly();
    private readonly List<VisitorPermit> _vistorPermits = [];
    public IReadOnlyCollection<VisitorPermit> VisitorPermits => _vistorPermits.AsReadOnly();

    private Visitor() { }
    private Visitor(string firstName, string lastName, string email,
        string phoneNumber, string nationalId, BranchId branchId, PersonGender gender)
    {
        VisitorId = new VisitorId(Guid.NewGuid());
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        NationalId = nationalId;
        BranchId = branchId;
        Gender = gender;
    }

    public static Visitor Create(string firstName, string lastName, string email,
        string phoneNumber, string nationalId, BranchId branchId, PersonGender gender)
        => new(firstName, lastName, email, phoneNumber, nationalId, branchId, gender);

    public void UpdateFirstName(string firstName) => FirstName = firstName;
    public void UpdateLastName(string lastName) => LastName = lastName;
    public void UpdateEmail(string email) => Email = email;
    public void UpdatePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;
    public void UpdateNationalId(string nationalId) => NationalId = nationalId;

}
