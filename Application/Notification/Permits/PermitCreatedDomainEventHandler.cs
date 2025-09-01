using Application.Services.Email;
using Domain.TenantDomain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitCreatedDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<NewPermitCreatedDomainEvent>
{
    public async Task Handle(NewPermitCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var email in notification.Emails)
        {
            var emailContent = new EmailContent(email, $"your permit with id {notification.PermitId} is pending right now.", "Permit status");

            await emailService.SendEmail(emailContent);
        }
    }
}
