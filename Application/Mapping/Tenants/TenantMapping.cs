namespace Application.Tenants;

public partial class TenantMapping : Profile
{
    public TenantMapping()
    {
        Map();
        MapDetails();
    }
}
