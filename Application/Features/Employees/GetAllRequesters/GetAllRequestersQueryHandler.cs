using Application.Users.Dtos;
using Domain.TenantDomain.Users.Repositories.Requesters;

namespace Application.Features.Employees.GetAllRequesters;

public sealed class GetAllRequestersQueryHandler(
    IRequesterQueryRepository securityQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllRequesterQuery, Response<List<GetUserDto>>>
{
    public async Task<Response<List<GetUserDto>>> Handle(GetAllRequesterQuery request, CancellationToken cancellationToken)
    {
        var securities = await securityQueryRepository.GetAllAsync();

        if (securities is null || securities.Count == 0)
            return NotFound<List<GetUserDto>>("There is no requesters.");

        var dtos = mapper.Map<List<GetUserDto>>(securities);

        return Success(dtos);
    }
}


