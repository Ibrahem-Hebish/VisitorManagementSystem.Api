using Application.Services.UnitOfWork;
using Domain.Users.Repositories.Managers;

namespace Application.BranchAdmin.DeleteManager;

public sealed class DeleteManagerCommandHandler(
    IManagerQueryRepository managerQueryRepository,
    IManagerCommandRepository managerCommandRepository,
    IUnitOfWork unitOfWork
    )
    : ResponseHandler,

    IRequestHandler<DeleteManagerCommand, Response<string>>
{
    public async Task<Response<string>> Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = await managerQueryRepository.GetByIdAsync(new UserId(new Guid(request.Id)));

        if (manager is null)
            return NotFouned<string>("There is no manager with that id.");

        managerCommandRepository.DeleteAsync(manager);

        manager.RaiseEmployeeDeletedDomainEvent(request.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Deleted<string>("Manager Deleted Successfully.");
    }
}
