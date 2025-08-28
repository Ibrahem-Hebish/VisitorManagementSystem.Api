using Application.Services.Email;
using Domain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitCreatedDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<NewPermitCreatedDomainEvent>
{
    public Task Handle(NewPermitCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
