using Domain.EntryLogs;

namespace Domain.Users;

public class Security(string firstName, string lastName, string email, string hashedPassword
                                   , string phoneNumber)

    : Employee(firstName, lastName, email, hashedPassword, phoneNumber)
{
    private readonly List<Entrylog> _entrylogs = [];
    public IReadOnlyCollection<Entrylog> Entrylogs => _entrylogs.AsReadOnly();

}