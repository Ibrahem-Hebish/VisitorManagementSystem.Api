namespace Application.Dtos.Tenant;

public record TenantDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public bool HasSoloDb { get; set; }

}

