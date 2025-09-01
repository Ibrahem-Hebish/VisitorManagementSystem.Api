using Application.Dtos.Visitors;
using Domain.TenantDomain.Visitors.Repositories;

namespace Application.Features.Visitors.GetAllVisitors;

public sealed class GetAllVisitorsQueryHandler(
    IVisitorQueryRepository visitorQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllVisitorsQuery, Response<List<VisitorDto>>>
{
    public async Task<Response<List<VisitorDto>>> Handle(GetAllVisitorsQuery request, CancellationToken cancellationToken)
    {
        var visitors = await visitorQueryRepository.GetAllAsync(cancellationToken);

        if(visitors.Count == 0 || visitors is null)
            return NotFound<List<VisitorDto>>("There is no visitors.");

        var dtos = mapper.Map<List<VisitorDto>>(visitors);

        return Success(dtos);
    }
}
