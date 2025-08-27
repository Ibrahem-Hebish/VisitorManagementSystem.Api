
namespace Domain.Tenants;

public class Tenant : Entity
{
    public TenantId Id { get; private set; }
    public string Name { get; private set; }
    public string ConnectionString { get; private set; }

    private List<Branch> _branches = [];
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

}

