using Domain.CatalogDb.Branches;

namespace Domain.CatalogDb.Tenants;

public class SharedTenant
{
    public SharedTenantId Id { get; private set; }
    public string Name { get; private set; }
    public string ConnectionString { get; private set; }

    private readonly List<SharedBranch> _branches = [];
    public IReadOnlyCollection<SharedBranch> Branches => _branches.AsReadOnly();
    public SharedUserId? ManagerId { get; private set; }
    public SharedUser? Manager { get; private set; }

    private SharedTenant() { }

    private SharedTenant(string name, string connectionString)
    {
        Id = new SharedTenantId(Guid.NewGuid());
        Name = name;
        ConnectionString = connectionString;
    }

    private SharedTenant(SharedTenantId id, string name, string connectionString)

    {
        Id = id;
        Name = name;
        ConnectionString = connectionString;
    }

    public static SharedTenant Create(string name, string connectionString) => new(name, connectionString);
    public static SharedTenant Create(SharedTenantId id, string name, string connectionString) => new(id, name, connectionString);


    public void UpdateName(string name) => Name = name;

    public void SetId(SharedTenantId sharedTenantId)
    {
        Id = sharedTenantId;
    }

    public void SetManager(SharedUserId managerId)
    {
        ManagerId = managerId;
    }
}
