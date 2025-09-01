using Domain.TenantDomain.Attachments;
using Domain.TenantDomain.Belongings;
using Domain.TenantDomain.Branches.DomainEvents;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Buildings;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.PermitTracks;
using Domain.TenantDomain.PermitUpdateRequests;
using Domain.TenantDomain.Tenants;
using Domain.TenantDomain.Tenants.ObjectValues;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;
using Domain.TenantDomain.VisitorPermits;
using Domain.TenantDomain.Visitors;

namespace Domain.TenantDomain.Branches;

public class Branch : Entity
{
    public BranchId Id { get; private set; }
    public string Name { get; private set; }
    public BranchAddress Address { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public TenantId TenantId { get; private set; }
    public Tenant Tenant { get; private set; }
    private readonly List<Visitor> _visitors = [];
    public UserId? ManagerId { get; private set; }
    public Employee? Manager { get; private set; }
    public IReadOnlyCollection<Visitor> Visitors => _visitors.AsReadOnly();
    private readonly List<VisitorPermit> _vistorPermits = [];
    public IReadOnlyCollection<VisitorPermit> VisitorPermits => _vistorPermits.AsReadOnly();

    private readonly List<Building> _buildings = [];
    public IReadOnlyCollection<Building> Buildings => _buildings.AsReadOnly();
    private readonly List<Employee> _employees = [];
    public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();
    private readonly List<Entrylog> _entrylogs = [];
    public IReadOnlyCollection<Entrylog> EntryLogs => _entrylogs.AsReadOnly();
    private readonly List<Permit> _permits = [];
    public IReadOnlyCollection<Permit> Permits => _permits.AsReadOnly();
    private readonly List<Attachment> _attachments = [];
    public IReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();

    private readonly List<Belonging> _belongings = [];
    public IReadOnlyCollection<Belonging> Belongings => _belongings.AsReadOnly();
    private readonly List<PermitTrack> _permitTracks = [];
    public IReadOnlyCollection<PermitTrack> PermitTrack => _permitTracks.AsReadOnly();
    private readonly List<PermitUpdateRequest> _permitUpdateRequests = [];
    public IReadOnlyCollection<PermitUpdateRequest> PermitUpdateRequest => _permitUpdateRequests.AsReadOnly();

    private Branch() { }
    private Branch(string name, BranchAddress address, string phoneNumber, string email)
    {
        Id = new BranchId(Guid.NewGuid());
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public static Branch Create(string name, BranchAddress address, string phone, string email)
                                                                             => new(name, address, phone, email);

    public void UpdateName(string name) => Name = name;
    public void UpdateAddress(BranchAddress address) => Address = address;
    public void UpdatePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;
    public void UpdateEmail(string email) => Email = email;
    public void SetTenant(TenantId tenantId)
    {
        TenantId = tenantId;
    }
    public void SetManager(UserId managerId)
    {
        ManagerId = managerId;
    }
    public void UnsetManager()
    {
        ManagerId = null;
    }

    public void RaiseNewBranchDomainEvent()
    {
        Raise(new NewBranchCreatedDomainEvent(TenantId.Value, Id.Value, Name, Address, PhoneNumber, Email));
    }

}

