namespace Application.Services.PasswordHashing;

public interface IPasswordHashingService
{
    string HashPasswordBCrypt(string password);
    bool VerifyPasswordBCrypt(string password, string hashedPassword);
}