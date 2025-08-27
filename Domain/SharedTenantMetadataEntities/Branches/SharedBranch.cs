using Domain.SharedTenantMetadataEntities.UserTokens;

namespace Domain.SharedTenantMetadataEntities.Branches;

public class SharedBranch
{
    public SharedBranchId Id { get; private set; }
    public string Name { get; private set; }
    public SharedBranchAddress Address { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public SharedTenantId TenantId { get; private set; }
    public SharedTenant Tenant { get; private set; }
    private List<SharedUser> _users = [];
    public IReadOnlyCollection<SharedUser> Users => _users.AsReadOnly();

    private readonly List<SharedUserToken> _sharedUserTokens = [];
    public IReadOnlyCollection<SharedUserToken> sharedUserTokens => _sharedUserTokens.AsReadOnly();

    private SharedBranch() { }
    private SharedBranch(string name, SharedBranchAddress address, string phoneNumber, string email)
    {
        Id = new(Guid.NewGuid());
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    private SharedBranch(SharedBranchId id, string name, SharedBranchAddress address, string phoneNumber, string email)
    {
        Id = id;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public static SharedBranch Create(string name, SharedBranchAddress address, string phone, string email)
                                                                             => new(name, address, phone, email);

    public static SharedBranch Create(SharedBranchId id, string name, SharedBranchAddress address, string phone, string email)
                                                                               => new(id, name, address, phone, email);

    public void UpdateName(string name) => Name = name;
    public void UpdateAddress(SharedBranchAddress address) => Address = address;
    public void UpdatePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;
    public void UpdateEmail(string email) => Email = email;
    public void AddUser(SharedUser user) => _users.Add(user);

    public void RemoveUser(SharedUser user) => _users.Remove(user);

    public void SetTenant(SharedTenant tenant)
    {
        Tenant = tenant;
        TenantId = tenant.Id;
    }
}
