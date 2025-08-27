using Domain.Visitors.ObjectValues;

namespace Domain.Visitors.Repositories;

public interface IVisitorCommandRepository
{
    Task AddAsync(Visitor visitor);
    Task UpdateAsync(Visitor visitor);
    Task DeleteAsync(VisitorId visitorId);
}
