using Application.Dtos.Buildings;

namespace Application.Features.Buildings.GetBuildingById;

public sealed record GetBuildingByIdQuery(string Id) : IRequest<Response<GetBuildingDto>>,IValidatorRequest;
