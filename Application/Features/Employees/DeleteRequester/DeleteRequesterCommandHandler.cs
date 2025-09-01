using Domain.TenantDomain.Users.Repositories.Requesters;

namespace Application.Features.Employees.DeleteRequester;

public sealed class DeleteRequesterCommandHandler(
    IRequesterQueryRepository requesterQueryRepository,
    IRequesterCommandRepository requesterCommandRepository,
    IUnitOfWork unitOfWork
    )
    : ResponseHandler,

    IRequestHandler<DeleteRequesterCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteRequesterCommand request, CancellationToken cancellationToken)
    {
        var requester = await requesterQueryRepository.GetByIdAsync(new UserId(new Guid(request.Id)));

        if (requester is null)
            return NotFound<string>("There is no requester with that id.");

        requesterCommandRepository.DeleteAsync(requester);

        requester.RaiseEmployeeDeletedDomainEvent(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Deleted<string>("Requester Deleted Successfully.");
    }
}
