using Application.Users.Dtos;
using Domain.CatalogDb.SharedUsers.ObjectValues;

namespace Application.Features.Authentication.GetProfile;

public class GetUserInfoQueryHandler(
    ISharedUserQueryRepository sharedUserQueryRepository,
    IMapper mapper
    )
    : ResponseHandler,
    IRequestHandler<GetUserInfoQuery, Response<GetUserDto>>
{
    public async Task<Response<GetUserDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await sharedUserQueryRepository.GetByIdAsync(new SharedUserId(new Guid(request.UserId)), cancellationToken);

        if (user is null)
            return NotFound<GetUserDto>("User is not found");

        var userDto = mapper.Map<GetUserDto>(user);

        return Success(userDto);

    }
}
