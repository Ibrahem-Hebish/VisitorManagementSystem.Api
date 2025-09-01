namespace Application.Dtos.Attachments;

public record CreateAttachment(
    string Type,
    IFormFile File);
