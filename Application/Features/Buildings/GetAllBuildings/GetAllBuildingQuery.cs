using Application.Dtos.Buildings;

namespace Application.Features.Buildings.GetAllBuildings;

public sealed record GetAllBuildingQuery : IRequest<Response<List<GetBuildingDto>>>;
