using Domain.TenantDomain.Branches;
using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Common;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Permits.ObjectValues;
using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.ObjectValues;

namespace Domain.TenantDomain.VisitorPermits;

public class VisitorPermit : IMultiTenant
{
    public VisitorId VisitorId { get; set; }
    public Visitor Visitor { get; set; }

    public PermitId PermitId { get; set; }
    public Permit Permit { get; set; }
    public BranchId BranchId { get; set; }
    public Branch Branch { get; set; }
}
