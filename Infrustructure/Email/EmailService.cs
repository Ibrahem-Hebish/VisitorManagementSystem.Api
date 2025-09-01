
namespace Infrustructure.Email;

public class EmailService(IOptions<EmailSettings> options,
    IConfiguration configuration) : IEmailService
{
    public async Task SendEmail(EmailContent emailContent)
    {
        try
        {
            using var client = new SmtpClient();
            {
                await client.ConnectAsync(
                options.Value.ClientEmail,
                options.Value.Port,
                SecureSocketOptions.StartTls);

                client.Authenticate(
                    options.Value.Email,
                    configuration["emailpassword"]);

                var bodyBuilder = new BodyBuilder()
                {
                    HtmlBody = $"<div><h2>{emailContent.Message}</h2></div>",
                    TextBody = $"Welcome {emailContent.Email}"
                };

                var mimeMessage = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody(),
                    Subject = emailContent.Subject
                };

                mimeMessage.From.Add(new MailboxAddress(
                    options.Value.Name,
                    options.Value.Email));

                mimeMessage.To.Add(new MailboxAddress(
                    emailContent.Email[..emailContent.Email.IndexOf('@')],
                    emailContent.Email));

                await client.SendAsync(mimeMessage);

                await client.DisconnectAsync(true);

            }
        }
        catch
        {
            Log.Warning($"Sending email to {emailContent.Email} failed");
        }
    }

    public async Task SendEmailWithPdfAsync(EmailContent emailContent, byte[] pdfBytes, string pdfFileName = "Permit.pdf")
    {
        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(
                options.Value.ClientEmail,
                options.Value.Port,
                SecureSocketOptions.StartTls);

            client.Authenticate(
                options.Value.Email,
                configuration["emailpassword"]);

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<div><h2>{emailContent.Message}</h2></div>",
                TextBody = $"Please find the attached PDF document."
            };

            // Attach the PDF directly from bytes
            bodyBuilder.Attachments.Add(pdfFileName, pdfBytes);

            var mimeMessage = new MimeMessage
            {
                Body = bodyBuilder.ToMessageBody(),
                Subject = emailContent.Subject
            };

            mimeMessage.From.Add(new MailboxAddress(options.Value.Name, options.Value.Email));
            mimeMessage.To.Add(MailboxAddress.Parse(emailContent.Email));

            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Log.Warning(ex, $"Sending email with PDF to {emailContent.Email} failed");
        }
    }


}

