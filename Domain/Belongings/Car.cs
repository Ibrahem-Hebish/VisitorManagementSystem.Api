namespace Domain.Belongings;

public class Car(string name, string description,
    VisitorId visitorId, PermitId permitId, string plateNumber, string color)
    : Belonging(name, description, visitorId, permitId)
{
    public string PlateNumber { get; private set; } = plateNumber;
    public string Color { get; private set; } = color;

}
