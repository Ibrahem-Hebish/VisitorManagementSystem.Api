namespace Application.Services.EncryptionService;

public interface IConnectionStringProtector
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}
