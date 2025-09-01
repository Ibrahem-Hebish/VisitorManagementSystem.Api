namespace Domain.CatalogDb.UserTokens.Repositories;

public interface ISharedUserTokenCommandRepository
{
    Task AddAsync(SharedUserToken userToken);
    Task DeleteAsync(SharedUserToken userToken);
    Task UpdateAsync(SharedUserToken userToken);

}
