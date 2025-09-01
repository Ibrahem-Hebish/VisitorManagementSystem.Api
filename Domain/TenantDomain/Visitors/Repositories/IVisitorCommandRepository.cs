using Domain.TenantDomain.Visitors;

namespace Domain.TenantDomain.Visitors.Repositories;

public interface IVisitorCommandRepository
{
    Task AddAsync(Visitor visitor);
    void Update(Visitor visitor);
    void Delete(Visitor visitor);
}
