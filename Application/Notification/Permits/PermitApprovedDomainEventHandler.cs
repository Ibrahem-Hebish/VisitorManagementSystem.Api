using Application.Dtos.Permits;
using Application.Services.Email;
using Application.Services.Pdf;
using Domain.TenantDomain.Permits.DomainEvents;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Notification.Permits;

public sealed class PermitApprovedDomainEventHandler(
    IEmailService emailService,
    IPermitQueryRepository permitQueryRepository,
    IPdfTranslationService pdfTranslationService,
    IMapper mapper
    )
    : INotificationHandler<PermitApprovedDomainEvent>
{
    public async Task Handle(PermitApprovedDomainEvent notification, CancellationToken cancellationToken)
    {
        var permit = await permitQueryRepository.GetByIdAsync(notification.PermitId, cancellationToken);

        var dto = mapper.Map<PermitDto>(permit);

        var pdf = pdfTranslationService.GeneratePermitPdf(dto);

        foreach (var email in notification.Emails)
        {
            var emailContent = new EmailContent(email, $"your permit with id {notification.PermitId} is approved", "Permit status");

            await emailService.SendEmailWithPdfAsync(emailContent, pdf);
        }
    }
}
