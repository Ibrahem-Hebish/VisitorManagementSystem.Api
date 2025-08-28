using Application.Dtos.Permits;
using Domain.Permits.ObjectValues;
using Domain.Permits.Repositories;

namespace Application.Permits.Queries.GetPermitDetails;

public sealed class GetPermitDetailsHandler(
    IPermitQueryRepository permitQueryRepository,
    IMapper mapper)

    : ResponseHandler,

    IRequestHandler<GetPermitDetails, Response<PermitDetailsDto>>
{
    public async Task<Response<PermitDetailsDto>> Handle(GetPermitDetails request, CancellationToken cancellationToken)
    {
        var permit = await permitQueryRepository.GetDetailsAsync(new PermitId(new Guid(request.PermitId)),cancellationToken);

        if(permit is null)
            return NotFouned<PermitDetailsDto>();

        var permitDetais = mapper.Map<PermitDetailsDto>(permit);

        return Success(permitDetais);
    }
}
