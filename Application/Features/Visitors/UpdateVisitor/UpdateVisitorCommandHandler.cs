using Domain.TenantDomain.Visitors.ObjectValues;
using Domain.TenantDomain.Visitors.Repositories;

namespace Application.Features.Visitors.UpdateVisitor;

public sealed class UpdateVisitorCommandHandler(
    IVisitorQueryRepository visitorQueryRepository,
    IVisitorCommandRepository visitorCommandRepository,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<UpdateVisitorCommand, Response<string>>
{
    public async Task<Response<string>> Handle(UpdateVisitorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var visitor = await visitorQueryRepository.GetByIdAsync(new VisitorId(new Guid(request.Id)));

            if (visitor is null)
                return NotFound<string>("There is no visitor with this id.");

            if (visitor.Email != request.Email)
            {
                var visitorByEmail = await visitorQueryRepository.GetByEmailAsync(request.Email);

                if (visitorByEmail is not null)
                    return BadRequest<string>("There is a user with this email.");

                visitor.UpdateEmail(request.Email);
            }

            visitor.UpdateDetails(request.FirstName, request.LastName, request.NationalId, request.PhoneNumber);

            visitorCommandRepository.Update(visitor);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Success("Visitor updated successfully");
        }
        catch (Exception ex)
        {
            // Logs 

            return InternalServerError<string>();
        }
    }
}
