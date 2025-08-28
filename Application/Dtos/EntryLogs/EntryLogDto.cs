namespace Application.Dtos.EntryLogs;

public record EntryLogDto
{
    public string Id { get; set; }
    public DateTime EntryTime { get; set; }
    public bool IsInside { get; set; }
    public string VisitorName { get; set; }
    public string SecurityName { get; set; }
}
