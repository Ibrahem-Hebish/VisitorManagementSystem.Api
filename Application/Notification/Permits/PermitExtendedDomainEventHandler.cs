using Application.Services.Email;
using Domain.TenantDomain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitExtendedDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<PermitExtendedDomainEvent>
{
    public async Task Handle(PermitExtendedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var email in notification.Emails)
        {
            var emailContent = new EmailContent(email, $"your permit with id {notification.PermitId} is extenended to {notification.EndDate}", "Permit status");

            await emailService.SendEmail(emailContent);
        }
    }
}
