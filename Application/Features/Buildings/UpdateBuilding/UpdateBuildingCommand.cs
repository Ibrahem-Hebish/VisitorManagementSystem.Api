namespace Application.Features.Buildings.UpdateBuilding;

public sealed record UpdateBuildingCommand(string Id, string Name, int FloorsNumber) : IRequest<Response<string>>, IValidatorRequest;
