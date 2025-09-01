using Domain.TenantDomain.EntryLogs;
using Domain.TenantDomain.Users.Enums;

namespace Domain.TenantDomain.Users;

public class Security(string firstName, string lastName, string email, string hashedPassword
                                   , string phoneNumber, PersonGender gender)

    : Employee(firstName, lastName, email, hashedPassword, phoneNumber, gender)
{
    private readonly List<Entrylog> _entrylogs = [];
    public IReadOnlyCollection<Entrylog> Entrylogs => _entrylogs.AsReadOnly();

}