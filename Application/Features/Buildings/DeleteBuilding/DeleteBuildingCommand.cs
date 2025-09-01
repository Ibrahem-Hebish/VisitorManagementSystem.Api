namespace Application.Features.Buildings.DeleteBuilding;

public sealed record DeleteBuildingCommand(string Id) : IRequest<Response<string>>, IValidatorRequest;
