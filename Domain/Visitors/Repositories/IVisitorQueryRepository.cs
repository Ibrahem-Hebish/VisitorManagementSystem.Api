namespace Domain.Visitors.Repositories;

public interface IVisitorQueryRepository
{
    Task<Visitor?> GetByIdAsync(VisitorId visitorId);
    Task<List<Permit>> GetVisitorPermitsAsync(VisitorId visitorId);
    Task<List<Permit>> GetVisitorPermits(VisitorId visitorId);
    Task<List<Visitor>> GetExsistingVisitors(List<string> emails, CancellationToken cancellationToken = default);
    Task<bool> ExsistsAsync(VisitorId visitorId);
    Task<List<Visitor>> GetAllAsync();
    Task<IEnumerable<Visitor>> GetByBranchIdAsync(BranchId branchId);
}
