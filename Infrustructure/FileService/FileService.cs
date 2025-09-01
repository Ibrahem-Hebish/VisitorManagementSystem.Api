namespace Infrustructure.FileService;

public class FileService(IWebHostEnvironment env) : IFileService
{
    private readonly string _webRootPath = env.WebRootPath;

    public async Task<string> UploadAsync(IFormFile file, string subDirectory)
    {
        var folderPath = Path.Combine(_webRootPath, subDirectory);

        Directory.CreateDirectory(folderPath);

        var filePath = Path.Combine(folderPath, file.FileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/{subDirectory}/{file.FileName}";
    }

    public Task<bool> DeleteAsync(string filePath)
    {
        var fullPath = Path.Combine(_webRootPath, filePath.TrimStart('/'));

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }


}


