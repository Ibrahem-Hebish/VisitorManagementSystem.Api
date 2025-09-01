using Application.Dtos.Permits;

namespace Application.Services.Pdf;

public interface IPdfTranslationService
{
    byte[] GeneratePermitPdf(PermitDto permitDto);
}
