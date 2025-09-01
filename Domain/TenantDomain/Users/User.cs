using Domain.TenantDomain.Common;
using Domain.TenantDomain.Roles;
using Domain.TenantDomain.Roles.ObjectValues;
using Domain.TenantDomain.Tokens;
using Domain.TenantDomain.Users.Enums;
using Domain.TenantDomain.Users.ObjectValues;

namespace Domain.TenantDomain.Users;
public class User : Entity
{
    public UserId Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public string PhoneNumber { get; private set; }
    public PersonGender Gender { get; private set; }
    public RoleId RoleId { get; private set; }
    public Role Role { get; private set; }
    private readonly List<UserToken> _userTokens = [];
    public IReadOnlyCollection<UserToken> UserTokens => _userTokens.AsReadOnly();

    private User() { }
    public User(string firstName, string lastName, string email, string hashedPass, string phoneNumber, PersonGender gender)
    {
        Id = new UserId(Guid.NewGuid());
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        HashedPassword = hashedPass;
        Gender = gender;
    }

    public void UpdateFirstName(string firstName) => FirstName = firstName;
    public void UpdateLastName(string lastName) => LastName = lastName;
    public void UpdateEmail(string email) => Email = email;
    public void UpdatePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;
    public void SetRole(Role role)
    {
        Role = role;
        RoleId = role.Id;
    }
    public void ChangePassword(string newHashedPassword)
    {
        HashedPassword = newHashedPassword;
    }



}
