namespace Application.Services.File;

public interface IFileService
{
    Task<string> UploadAsync(IFormFile file, string subDirectory);
    Task<bool> DeleteAsync(string filePath);
}
