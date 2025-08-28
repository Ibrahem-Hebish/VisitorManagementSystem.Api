namespace Application.Dtos.Attachments;

public record CreateAttachment
{
    public string Type { get; set; }
    public IFormFile File { get; set; }
}
