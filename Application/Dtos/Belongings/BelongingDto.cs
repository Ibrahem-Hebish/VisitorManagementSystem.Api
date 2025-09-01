namespace Application.Dtos.Belongings;

public record BelongingDto(
    string Id,
    string Name,
    string Description,
    string? PlateNumber,
    string? Color
    );