using Domain.Belongings.ObjectValues;

namespace Domain.Belongings.Repositories;

public interface IBelongingQueryRepository
{
    Task<Belonging?> GetByIdAsync(BelongingId belongingId);
    Task<List<Belonging>> GetAllAsync();
}
