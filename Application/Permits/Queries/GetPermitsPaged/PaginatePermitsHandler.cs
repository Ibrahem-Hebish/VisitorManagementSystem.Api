using Application.Dtos.Permits;
using Domain.Permits.ObjectValues;
using Domain.Permits.Repositories;

namespace Application.Permits.Queries.GetPermitsPaged;
public sealed class PaginatePermitsHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<PaginatePermits, Response<IReadOnlyList<PermitDto>>>
{
    public async Task<Response<IReadOnlyList<PermitDto>>> Handle(PaginatePermits request, CancellationToken cancellationToken)
    {
        var id = request.Id is null ? null : new PermitId(new Guid(request.Id));

        var permits = await permitQueryRepository.GetPagedAsync(
                                                  id,
                                                  request.CursorDate,
                                                  request.Direction,
                                                  cancellationToken);

        if (permits.Count == 0)
            return NotFouned<IReadOnlyList<PermitDto>>();

        var dtos = mapper.Map<IReadOnlyList<PermitDto>>(permits);

        return Success(dtos);
    }
}
