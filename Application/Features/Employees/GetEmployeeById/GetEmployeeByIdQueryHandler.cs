using Application.Users.Dtos;
using Domain.TenantDomain.Users.Repositories.Employees;

namespace Application.Features.Employees.GetEmployeeById;

public sealed class GetEmployeeByIdQueryHandler(
    IEmployeeQueryRepository employeeQueryRepository,
    IMapper mapper)

    : ResponseHandler,
    IRequestHandler<GetEmployeeByIdQuery, Response<GetUserDto>>
{
    public async Task<Response<GetUserDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var requester = await employeeQueryRepository.GetByIdAsync(new UserId(new Guid(request.Id)));

        if (requester is null)
            return NotFound<GetUserDto>("There is no requester with this id.");

        var dto = mapper.Map<GetUserDto>(requester);

        return Success(dto);


    }
}


