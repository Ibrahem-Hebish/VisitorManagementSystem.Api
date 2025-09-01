using Application.Users.Dtos;
using Domain.TenantDomain.Users.Repositories.Securities;

namespace Application.Features.Employees.GetAllSecurities;

public sealed class GetAllSecuritiesQueryHandler(
    ISecurityQueryRepository securityQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetAllSecuritiesQuery, Response<List<GetUserDto>>>
{
    public async Task<Response<List<GetUserDto>>> Handle(GetAllSecuritiesQuery request, CancellationToken cancellationToken)
    {
        var securities = await securityQueryRepository.GetAllAsync();

        if(securities is null || securities.Count == 0) 
            return NotFound<List<GetUserDto>>("There is no securities.");

        var dtos = mapper.Map<List<GetUserDto>>(securities);

        return Success(dtos);
    }
}


