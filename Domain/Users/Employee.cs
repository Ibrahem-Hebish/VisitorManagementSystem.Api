using Domain.Users.DomainEvents;
using Domain.Users.Enums;

namespace Domain.Users;

public class Employee(string firstName, string lastName, string email, string hashedPassword,
                          string phoneNumber)
    : User(firstName, lastName, email, hashedPassword, phoneNumber), IMultiTenant
{
    public EmployeePosition Position { get; private set; }
    public UserId? ManagerId { get; private set; }
    public Employee? Manager { get; private set; }
    private readonly List<PermitTrack> _permitTracks = [];
    public IReadOnlyCollection<PermitTrack> PermitTracks => _permitTracks.AsReadOnly();
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; }

    public void SetPosition(EmployeePosition position) => Position = position;
    public void UpdatePosition(EmployeePosition position) => Position = position;

    public void SetBranch(Branch branch)
    {
        Branch = branch;
        BranchId = branch.Id;
    }
    public void SetManager(UserId managerId)
    {
        ManagerId = managerId;
    }
    public void RaiseUserCreatedDomainEvent(BranchId branchId)
    {
        Raise(new NewEmployeeCreatedDomainEvent()
        {
            Id = Id.Id,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Gender = Gender,
            PhoneNumber = PhoneNumber,
            BranchId = branchId
        });
    }

    public void RaiseEmployeeDeletedDomainEvent(string id)
    {
        Raise(new EmployeeDeletedDomainEvent(id));
    }
}
