using Application.Services.Email;
using Domain.TenantDomain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitRejectedDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<PermitRejectedDomainEvent>
{
    public async Task Handle(PermitRejectedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var email in notification.Emails)
        {
            var emailContent = new EmailContent(email, $"your permit with id {notification.PermitId} is rejected", "Permit status");

            await emailService.SendEmail(emailContent);
        }
    }
}
