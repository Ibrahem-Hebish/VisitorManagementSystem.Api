using Application.Services.Email;
using Domain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitExpiredDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<PermitExpiredDomainEvent>
{
    public Task Handle(PermitExpiredDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
