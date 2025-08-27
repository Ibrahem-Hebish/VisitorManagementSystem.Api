namespace Infrustructure.HashingPassword;

public class PasswordHashingService : IPasswordHashingService
{
    public string HashPasswordBCrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPasswordBCrypt(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
