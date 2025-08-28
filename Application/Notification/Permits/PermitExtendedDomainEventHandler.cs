using Application.Services.Email;
using Domain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitExtendedDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<PermitExtendedDomainEvent>
{
    public Task Handle(PermitExtendedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
