using Domain.Attachments.ObjectValues;

namespace Domain.Attachments.Repositories;

public interface IAttachmentCommandRepository
{
    Task AddAsync(Attachment attachment);
    Task UpdateAsync(Attachment attachment);
    Task DeleteAsync(AttachmentId attachmentId);
}
