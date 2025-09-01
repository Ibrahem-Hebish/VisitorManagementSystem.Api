using Application.Users.Dtos;
using Domain.TenantDomain.Users.Repositories.Managers;

namespace Application.Features.Employees.GetAllManagers;

public sealed record GetAllManagersQuery : IRequest<Response<List<GetUserDto>>>;


public sealed class GetAllManagersQueryHandler(
    IManagerQueryRepository securityQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllManagersQuery, Response<List<GetUserDto>>>
{
    public async Task<Response<List<GetUserDto>>> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
    {
        var securities = await securityQueryRepository.GetAllAsync();

        if (securities is null || securities.Count == 0)
            return NotFound<List<GetUserDto>>("There is no requesters.");

        var dtos = mapper.Map<List<GetUserDto>>(securities);

        return Success(dtos);
    }
}


