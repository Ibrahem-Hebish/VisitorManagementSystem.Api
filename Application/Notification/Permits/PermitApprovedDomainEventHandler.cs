using Application.Services.Email;
using Domain.Permits.DomainEvents;

namespace Application.Notification.Permits;

public sealed class PermitApprovedDomainEventHandler(
    IEmailService emailService
    )
    : INotificationHandler<PermitApprovedDomainEvent>
{
    public Task Handle(PermitApprovedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
