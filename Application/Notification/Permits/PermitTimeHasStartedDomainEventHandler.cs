using Application.Services.Email;
using Domain.TenantDomain.EntryLogs.DomainEvents;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Notification.Permits;

public class PermitTimeHasStartedDomainEventHandler(
    IPermitQueryRepository permitQueryRepository,
    IEmailService emailService)
    
    : INotificationHandler<PermitTimeHasStartedDomainEvent>
{
    public async Task Handle(PermitTimeHasStartedDomainEvent notification, CancellationToken cancellationToken)
    {
        var permit = await permitQueryRepository.GetByIdAsync(notification.PermitId, cancellationToken);

        if(permit is not null)
        {
            foreach(var visitor in permit.Visitors)
            {
                var content = new EmailContent(visitor.Email, $"Permit with id {permit.PermitId} has started.", "Permit Status");

                await emailService.SendEmail(content);
            }

            // Trigger background service to notify visitors when the permit is about to end
        }
    }
}
