using Domain.TenantDomain.Visitors;
using Domain.TenantDomain.Visitors.Repositories;

namespace Application.Features.Visitors.CreateNewVisitor;

public sealed class CreateVisitorCommandHandler(
    IVisitorQueryRepository visitorQueryRepository,
    IVisitorCommandRepository visitorCommandRepository,
    IUnitOfWork unitOfWork)

    : ResponseHandler,
    IRequestHandler<CreateVisitorCommand, Response<string>>
{
    public async Task<Response<string>> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exsisitingVisitor = await visitorQueryRepository.GetByEmailAsync(request.Email);

            if (exsisitingVisitor is not null)
                return NotFound<string>("There is a visitor with that email.");

            var visitor = Visitor.Create(request.FirstName, request.LastName, request.Email,
                                        request.PhoneNumber, request.NationalId, request.Gender);

            await visitorCommandRepository.AddAsync(visitor);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Created<string>();
        }
        catch (Exception ex)
        {
            return InternalServerError<string>();
        }
    }
}



