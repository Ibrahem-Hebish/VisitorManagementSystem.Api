
namespace Domain.VisitorPermits;

public class VisitorPermit : IMultiTenant
{
    public VisitorId VisitorId { get; set; }
    public Visitor Visitor { get; set; }

    public PermitId PermitId { get; set; }
    public Permit Permit { get; set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; set; }
}
