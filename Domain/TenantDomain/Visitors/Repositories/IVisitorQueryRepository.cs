using Domain.TenantDomain.Branches.ObjectValues;
using Domain.TenantDomain.Permits;
using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.ObjectValues;

namespace Domain.TenantDomain.Visitors.Repositories;

public interface IVisitorQueryRepository
{
    Task<Visitor?> GetByIdAsync(VisitorId visitorId);

    Task<Visitor?> GetByEmailAsync(string email);
    Task<List<Permit>> GetVisitorPermitsAsync(VisitorId visitorId);
    Task<List<Permit>> GetVisitorPermits(VisitorId visitorId);
    Task<List<Visitor>> GetExsistingVisitors(List<string> emails, CancellationToken cancellationToken = default);
    Task<bool> ExsistsAsync(VisitorId visitorId);
    Task<List<Visitor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Visitor>> GetByBranchIdAsync(BranchId branchId);
}
