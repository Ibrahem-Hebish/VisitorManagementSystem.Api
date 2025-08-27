using Application.Services.TenantService;
using Application.UserTokens.Mapping;
using Application.Validation;
using MediatR;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(typeof(ITenantService).Assembly);
            opt.NotificationPublisher = new TaskWhenAllPublisher();
        });

        services.AddAutoMapper(cfg => { }, typeof(UserTokenProfile).Assembly);

        services.AddValidatorsFromAssemblyContaining<ITenantService>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        return services;
    }
}
