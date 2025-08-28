using Application.Dtos.Permits;

namespace Application.Permits.Queries.GetVisitorPermits;

public sealed class GetVistorPermitsQueryHandler(
    IVisitorQueryRepository visitorQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetVistorPermitsQuery, Response<List<PermitDto>>>
{
    public async Task<Response<List<PermitDto>>> Handle(GetVistorPermitsQuery request, CancellationToken cancellationToken)
    {
        var permits = await visitorQueryRepository.GetVisitorPermitsAsync(new VisitorId(new Guid(request.Id)));

        if (permits.Count == 0)
            return NotFouned<List<PermitDto>>();

        var dtos = mapper.Map<List<PermitDto>>(permits);

        return Success(dtos);
    }
}

