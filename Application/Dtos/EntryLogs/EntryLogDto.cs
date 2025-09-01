namespace Application.Dtos.EntryLogs;

public record EntryLogDto(
    string Id,
    DateTime EntryTime,
    bool IsInside,
    string SecurityName);
