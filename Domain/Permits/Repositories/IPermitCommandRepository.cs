namespace Domain.Permits.Repositories;

public interface IPermitCommandRepository
{
    Task CreateAsync(Permit permit, CancellationToken cancellationToken = default);
    void DeleteAsync(Permit permit);
    void UpdateAsync(Permit permit);
}
