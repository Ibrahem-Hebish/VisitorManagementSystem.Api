using Domain.TenantDomain.Visitors.ObjectValues;
using Domain.TenantDomain.Visitors.Repositories;

namespace Application.Features.Visitors.DeleteVisitor;

public sealed class DeleteVisitorCommandHandler(
    IVisitorQueryRepository visitorQueryRepository,
    IVisitorCommandRepository visitorCommandRepository,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<DeleteVisitorCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteVisitorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var visitor = await visitorQueryRepository.GetByIdAsync(new VisitorId(new Guid(request.Id)));

            if (visitor is null)
                return NotFound<string>();

            visitorCommandRepository.Delete(visitor);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Deleted<string>("Visitor is deleted successfully.");
        }
        catch (Exception ex)
        {
            // Logs

            return InternalServerError<string>();
        }
    }
}
