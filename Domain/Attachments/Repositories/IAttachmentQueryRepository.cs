using Domain.Attachments.ObjectValues;
using Domain.Permits.ObjectValues;
using Domain.Visitors.ObjectValues;

namespace Domain.Attachments.Repositories;

public interface IAttachmentQueryRepository
{
    Task<Attachment?> GetByIdAsync(AttachmentId attachmentId);
    Task<List<Attachment>> GetAllAsync();
    Task<IEnumerable<Attachment>> GetByVisitorIdAsync(VisitorId visitorId);
    Task<IEnumerable<Attachment>> GetByPermitIdAsync(PermitId permitId);
}
