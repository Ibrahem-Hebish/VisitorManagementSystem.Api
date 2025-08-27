using Domain.Permits;
using Domain.Visitors.ObjectValues;

namespace Domain.Visitors.Repositories;

public interface IVisitorQueryRepository
{
    Task<Visitor?> GetByIdAsync(VisitorId visitorId);
    Task<List<Permit>> GetVisitorPermitsAsync(VisitorId visitorId);
    Task<List<Visitor>> GetAllAsync();
    Task<IEnumerable<Visitor>> GetByBranchIdAsync(BranchId branchId);
}
