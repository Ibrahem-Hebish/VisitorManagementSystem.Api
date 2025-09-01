namespace Infrustructure.Pdf;
public class PdfTranslationService : IPdfTranslationService
{
    public byte[] GeneratePermitPdf(PermitDto permitDto)
    {
        return Document.Create(container =>
        {
            container.Page(page =>
            {
                // Page settings
                page.Size(PageSizes.A4);
                page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                // Header
                page.Header()
                    .Text("Permit Details")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Darken2);

                // Content
                page.Content()
                    .PaddingVertical(1, QuestPDF.Infrastructure.Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(10);
                        column.Item().Text($"Permit ID: {permitDto.PermitId}");
                        column.Item().Text($"Status: {permitDto.Status}");
                        column.Item().Text($"Reason: {permitDto.Reason}");

                        column.Item().PaddingTop(10).Text("Location").SemiBold();
                        column.Item().Text($"Building: {permitDto.BuldingName}");
                        column.Item().Text($"Floor: {permitDto.FloorNumber}");

                        column.Item().PaddingTop(10).Text("Dates").SemiBold();
                        column.Item().Text($"Start Date: {permitDto.StartDate:g}");
                        column.Item().Text($"End Date: {permitDto.EndDate:g}");
                        column.Item().Text($"Created At: {permitDto.CreatedAt:g}");
                        if (permitDto.UpdatedAt.HasValue)
                        {
                            column.Item().Text($"Updated At: {permitDto.UpdatedAt.Value:g}");
                        }

                        // Visitors Table
                        column.Item().PaddingTop(10).Text("Visitors").SemiBold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderStyle).Text("Name");
                                header.Cell().Element(HeaderStyle).Text("Email");
                                header.Cell().Element(HeaderStyle).Text("Phone Number");

                                static IContainer HeaderStyle(IContainer container)
                                {
                                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingBottom(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                                }
                            });

                            foreach (var visitor in permitDto.Visitors)
                            {
                                table.Cell().Element(CellStyle).Text($"{visitor.FirstName} {visitor.LastName}");
                                table.Cell().Element(CellStyle).Text(visitor.Email);
                                table.Cell().Element(CellStyle).Text(visitor.PhoneNumber);
                            }

                            static IContainer CellStyle(IContainer container) => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten3);
                        });
                    });

                // Footer
                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
            });
        }).GeneratePdf();
    }
}
