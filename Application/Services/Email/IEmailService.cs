namespace Application.Services.Email;

public interface IEmailService
{
    Task SendEmail(EmailContent emailContent);
    Task SendEmailWithPdfAsync(EmailContent emailContent, byte[] pdfBytes, string pdfFileName = "Permit.pdf");
}

