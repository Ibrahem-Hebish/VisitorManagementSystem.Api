using Application.Dtos.Permits;
using Domain.TenantDomain.Permits.Repositories;

namespace Application.Features.Permits.Queries.SearchByDate;

public sealed class SearchPermitByDateHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<SearchPermitByDateQuery, Response<List<PermitDto>>>
{
    public async Task<Response<List<PermitDto>>> Handle(SearchPermitByDateQuery request, CancellationToken cancellationToken)
    {
        var permits = await permitQueryRepository.SearchByDateAsync(request.StartDate, request.EndDate, cancellationToken);


        if (permits is null || permits.Count == 0)
            return NotFound<List<PermitDto>>();

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}
