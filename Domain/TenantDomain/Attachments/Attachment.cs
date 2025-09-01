using Domain.TenantDomain.Attachments.ObjectValues;
using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.ObjectValues;

namespace Domain.TenantDomain.Attachments;

public class Attachment : Entity, IMultiTenant
{
    public AttachmentId AttachmentId { get; private set; }
    public string Type { get; private set; } = "";
    public string FilePath { get; private set; } = "";
    public BranchId BranchId { get; set; }
    public Branch Branch { get; private set; } = null!;
    public PermitId PermitId { get; private set; }
    public Permit Permit { get; private set; } = null!;

    private Attachment() { }
    public Attachment(string type, string filePath, PermitId permitId)
    {
        AttachmentId = new AttachmentId(Guid.NewGuid());
        Type = type;
        FilePath = filePath;
        PermitId = permitId;
    }

    public static Attachment Create(string type, string filePath, PermitId permitId)
        => new(type, filePath, permitId);



}
