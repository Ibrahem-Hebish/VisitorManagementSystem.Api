namespace Presentation.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SkipTenantFilterAttribute : Attribute
{
}
