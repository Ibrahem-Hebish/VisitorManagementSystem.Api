namespace Application.Services.Email;

public class EmailContentWithFiles
{
    public string Email { get; set; } = string.Empty;   // Receiver email
    public string Subject { get; set; } = string.Empty; // Subject of the email
    public string Message { get; set; } = string.Empty; // Email body

    // List of file paths for attachments (PDF, images, etc.)
    public List<string> Attachments { get; set; } = new();
}