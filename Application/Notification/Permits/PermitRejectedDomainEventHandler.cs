using Application.Services.Email;
using Domain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitRejectedDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<PermitRejectedDomainEvent>
{
    public Task Handle(PermitRejectedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
