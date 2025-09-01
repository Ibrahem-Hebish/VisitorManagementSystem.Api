using Domain.TenantDomain.Attachments;
using Domain.TenantDomain.Attachments.ObjectValues;
using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.Visitors.ObjectValues;

namespace Domain.TenantDomain.Attachments.Repositories;

public interface IAttachmentQueryRepository
{
    Task<Attachment?> GetByIdAsync(AttachmentId attachmentId);
    Task<List<Attachment>> GetAllAsync();
    Task<IEnumerable<Attachment>> GetByVisitorIdAsync(VisitorId visitorId);
    Task<IEnumerable<Attachment>> GetByPermitIdAsync(PermitId permitId);
}
