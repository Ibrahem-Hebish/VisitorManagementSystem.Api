using Domain.TenantDomain.Attachments;
using Domain.TenantDomain.Attachments.ObjectValues;

namespace Domain.TenantDomain.Attachments.Repositories;

public interface IAttachmentCommandRepository
{
    Task AddAsync(Attachment attachment);
    Task UpdateAsync(Attachment attachment);
    Task DeleteAsync(AttachmentId attachmentId);
}
