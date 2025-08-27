using Domain.Roles.ObjectValues;

namespace Domain.Roles;

public class Role : Entity
{
    public RoleId Id { get; private set; }
    public string Name { get; private set; }
    private readonly List<User> _users = [];
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private Role() { }
    private Role(string name)
    {
        Id = new RoleId(Guid.NewGuid());
        Name = name;
    }

    public static Role Create(string name)
        => new(name);

    public void UpdateName(string name) => Name = name;
}
