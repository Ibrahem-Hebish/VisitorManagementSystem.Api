namespace Application.Dtos.Belongings;

public class CreateBelonging
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? PlateNumber { get; set; }
    public string? Color { get; set; }
}