using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Commands.DeletePermit;

public sealed class DeletePermitCommandHandler(
    IPermitQueryRepository permitQueryRepository,
    IPermitCommandRepository permitCommandRepository,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<DeletePermitCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeletePermitCommand request, CancellationToken cancellationToken)
    {

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var permit = await permitQueryRepository.GetByIdAsync(new PermitId(new Guid(request.PermitId)), cancellationToken);

            if (permit is null)
                return NotFound<string>();

            await permitCommandRepository.DeleteDependenciesAsync(request.PermitId);

            permitCommandRepository.Delete(permit);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await unitOfWork.CommitTransactionAsync();

            return Deleted<string>();
        }
        catch (Exception ex)
        {
            // Logs

            await unitOfWork.RollbackTransactionAsync();

            return InternalServerError<string>();
        }
    }
}
