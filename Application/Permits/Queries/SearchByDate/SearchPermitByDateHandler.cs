using Application.Dtos.Permits;
using Domain.Permits.Repositories;

namespace Application.Permits.Queries.SearchByDate;

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
            return NotFouned<List<PermitDto>>();

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}
