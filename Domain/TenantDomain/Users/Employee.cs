using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.PermitTracks;
using Domain.TenantDomain.Tenants;
using Domain.TenantDomain.Users.DomainEvents;
using Domain.TenantDomain.Users.Enums;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users;

public class Employee(string firstName, string lastName, string email, string hashedPassword,
                          string phoneNumber, PersonGender gender)
    : User(firstName, lastName, email, hashedPassword, phoneNumber, gender), IMultiTenant
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

    public void SetBranch(BranchId branchId)
    {
        BranchId = branchId;
    }


    public void SetManager(UserId managerId)
    {
        ManagerId = managerId;
    }
    public void RaiseUserCreatedDomainEvent(BranchId branchId)
    {
        Raise(new NewEmployeeCreatedDomainEvent()
        {
            Id = Id.Value,
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

    public void RaiseTenantAdminCreatedDomainEvent(string id, Tenant tenant)
    {
        Raise(new TenantAdminCreatedDomainEvent(id, tenant));
    }
}
