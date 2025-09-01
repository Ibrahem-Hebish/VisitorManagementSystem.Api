namespace Application.Services.Email;

public class EmailContent(string email, string message, string subject)
{
    public string Email { get; set; } = email;
    public string Message { get; set; } = message;
    public string Subject { get; set; } = subject;
}
