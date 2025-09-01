

namespace Application.Features.Permits.Queries.GetPermitById;

public sealed class GetPermitByIdHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetPermitByIdQuery, Response<PermitDto>>
{
    public async Task<Response<PermitDto>> Handle(GetPermitByIdQuery request, CancellationToken cancellationToken)
    {
        var permit = await permitQueryRepository.GetByIdAsync(new PermitId(new Guid(request.PermitId)), cancellationToken);

        if (permit is null)
            return NotFound<PermitDto>($"Permit with id {request.PermitId} is not found.");

        var dto = mapper.Map<PermitDto>(permit);

        return Success(dto);
    }
}
