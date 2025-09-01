namespace Application.Features.Buildings.CreateBuilding;

public sealed record CreateBuildingCommand(string Name, int FloorsNumber) : IRequest<Response<string>>, IValidatorRequest;
