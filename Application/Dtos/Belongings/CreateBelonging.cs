namespace Application.Dtos.Belongings;

public record CreateBelonging(
    string Name,
    string Description,
    string? PlateNumber,
    string? Color);