namespace Application.Dtos.Attachments;

public record AttachmentDto
{
    public string AttachmentId { get; set; }
    public string Type { get; set; }
    public string FilePath { get; set; }
}

