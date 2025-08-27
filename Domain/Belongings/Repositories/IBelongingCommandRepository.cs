using Domain.Belongings.ObjectValues;

namespace Domain.Belongings.Repositories;

public interface IBelongingCommandRepository
{
    Task AddAsync(Belonging belonging);
    Task UpdateAsync(Belonging belonging);
    Task DeleteAsync(BelongingId belongingId);
}
