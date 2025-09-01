using Domain.TenantDomain.Branches.ObjectValues;

namespace Domain.TenantDomain.Common;

public interface IMultiTenant
{
    public BranchId BranchId { get; set; }
}
