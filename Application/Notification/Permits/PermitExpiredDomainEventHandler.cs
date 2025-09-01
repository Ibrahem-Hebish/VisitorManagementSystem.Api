using Application.Services.Email;
using Domain.TenantDomain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitExpiredDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<PermitExpiredDomainEvent>
{
    public async Task Handle(PermitExpiredDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var email in notification.Emails)
        {
            var emailContent = new EmailContent(email, $"your permit with id {notification.PermitId} is expired", "Permit status");

            await emailService.SendEmail(emailContent);
        }
    }
}
