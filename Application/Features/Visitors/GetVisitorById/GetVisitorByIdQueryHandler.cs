

namespace Application.Features.Visitors.GetVisitorById;

public sealed class GetVisitorByIdQueryHandler(
    IVisitorQueryRepository visitorQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetVisitorByIdQuery, Response<VisitorDto>>
{
    public async Task<Response<VisitorDto>> Handle(GetVisitorByIdQuery request, CancellationToken cancellationToken)
    {
        var visitor = await visitorQueryRepository.GetByIdAsync(new VisitorId(new Guid(request.VisitorId)));

        if (visitor is null)
            return NotFound<VisitorDto>();

        var visitorDto = mapper.Map<VisitorDto>(visitor);

        return Success(visitorDto);
    }
}
