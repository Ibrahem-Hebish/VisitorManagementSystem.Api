using Application.Users.Dtos;
using Domain.TenantDomain.Users.Repositories.Requesters;

namespace Application.Features.Employees.GetAllEmployees;

public sealed class GetAllEmployeesQueryHandler(
    IRequesterQueryRepository securityQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllEmployeesQuery, Response<List<GetUserDto>>>
{
    public async Task<Response<List<GetUserDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var securities = await securityQueryRepository.GetAllAsync();

        if (securities is null || securities.Count == 0)
            return NotFound<List<GetUserDto>>("There is no requesters.");

        var dtos = mapper.Map<List<GetUserDto>>(securities);

        return Success(dtos);
    }
}


