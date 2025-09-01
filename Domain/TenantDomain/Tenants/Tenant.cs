using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Tenants.ObjectValues;
using Domain.TenantDomain.Users;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Tenants;

public class Tenant : Entity
{
    public TenantId Id { get; private set; }
    public string Name { get; private set; }
    public string ConnectionString { get; private set; }

    private List<Branch> _branches = [];
    public UserId? ManagerId { get; private set; }
    public Employee? Manager { get; private set; }
    public IReadOnlyCollection<Branch> Branches => _branches.AsReadOnly();



    private Tenant()
    {
    }

    private Tenant(string name, string connectionString)
    {
        Id = new TenantId(Guid.NewGuid());
        Name = name;
        ConnectionString = connectionString;
    }

    public static Tenant Create(string name, string connectionString) => new(name, connectionString);

    public void UpdateName(string name) => Name = name;

    public void SetManager(UserId managerId)
    {
        ManagerId = managerId;
    }

}

