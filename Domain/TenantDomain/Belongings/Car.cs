using Domain.TenantDomain.Permits.ObjectValues;

namespace Domain.TenantDomain.Belongings;

public class Car(string name, string description,
     PermitId permitId, string plateNumber, string color)
    : Belonging(name, description, permitId)
{
    public string PlateNumber { get; private set; } = plateNumber;
    public string Color { get; private set; } = color;

}
