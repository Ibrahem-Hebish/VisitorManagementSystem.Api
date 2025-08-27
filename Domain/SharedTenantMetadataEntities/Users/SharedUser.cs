using Domain.SharedTenantMetadataEntities.Branches;

namespace Domain.SharedTenantMetadataEntities.SharedUsers;

public class SharedUser
{
    public ObjectValues.SharedUserId Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public SharedBranchId? BranchId { get; private set; }
    public SharedBranch? Branch { get; private set; }
    private SharedUser() { }
    private SharedUser(string name, string email, string phoneNumber)
    {
        Id = new SharedUserId(Guid.NewGuid());
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;

    }



    private SharedUser(SharedUserId id, string name, string email, string phoneNumber, SharedBranch sharedBranch)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Branch = sharedBranch;
    }

    public static SharedUser Create(string name, string email, string phoneNumber)
        => new(name, email, phoneNumber);


    public static SharedUser Create(SharedUserId id, string name, string email, string phoneNumber, SharedBranch sharedBranch)
        => new(id, name, email, phoneNumber, sharedBranch);

    public void UpdateName(string name) => Name = name;
    public void UpdateEmail(string email) => Email = email;
    public void UpdatePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;

    public void SetBranch(SharedBranchId branchId)
    {
        BranchId = branchId;
    }

    public void SetId(SharedUserId id)
    {
        Id = id;
    }

}
