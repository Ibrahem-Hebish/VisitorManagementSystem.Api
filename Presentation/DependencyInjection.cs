using Application.Services.UnitOfWork;
using Domain.Permits.Repositories;
using Persistence.TenantDb.Repositories.Permits;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add<TenantFilter>();
        });

        services.AddScoped<ExeptionHandlerMiddleware>();

        services.AddDbContext<TenantDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetSection("TenantConnection").Value);
        });

        services.AddDbContext<SharedDbContext>((sp, options) =>
        {
            options.UseSqlServer(configuration.GetSection("SharedConnection").Value);
        });

        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IConnectionStringProtector, ConnectionStringProtector>();

        services.AddScoped<IFileService, FileService>();

        services.AddScoped<ITenantService, TenantService>();

        services.AddTransient<IPasswordHashingService, PasswordHashingService>();


        services.AddScoped<ITenantQueryRepository, TenantQueryRepository>();
        services.AddScoped<ITenantCommandRepository, TenantCommandRepository>();

        services.AddScoped<IBranchQueryRepository, BranchQueryRepository>();
        services.AddScoped<IBranchCommandRepository, BranchCommandRepository>();

        services.AddScoped<IRoleQueryRepository, RoleQueryRepository>();

        services.AddScoped<IUserQueryRepository, UserQueryRepository>();

        services.AddScoped<IUserTokenQueryRepository, UserTokenQueryRepository>();
        services.AddScoped<IUserTokenCommandRepository, UserTokenCommandRepository>();

        services.AddScoped<IEmployeeQueryRepository, EmployeeQueryRepository>();
        services.AddScoped<IEmployeeCommandRepository, EmpolyeeCommandRepository>();

        services.AddScoped<IRequesterQueryRepository, RequesterQueryRepository>();
        services.AddScoped<IRequesterCommandRepository, RequesterCommandRepository>();

        services.AddScoped<IManagerQueryRepository, ManagerQueryRepository>();
        services.AddScoped<IManagerCommandRepository, ManagerCommandRepository>();

        services.AddScoped<ISecurityQueryRepository, SecurityQueryRepository>();
        services.AddScoped<ISecurityCommandRepository, SecurityCommandRepository>();

        services.AddScoped<IPermitQueryRepository, PermitQueryRepository>();
        services.AddScoped<IPermitCommandRepository, PermitCommandRepository>();

        // Shared
        services.AddScoped<ISharedTenantCommandRepository, SharedTenantCommandRepository>();
        services.AddScoped<ISharedTenantQueryRepository, SharedTenantQueryRepository>();

        services.AddScoped<ISharedBranchQueryRepository, SharedBranchQueryRepository>();
        services.AddScoped<ISharedBranchCommandRepository, SharedBranchCommandRepository>();

        services.AddScoped<ISharedUserQueryRepository, SharedUserQueryRepository>();
        services.AddScoped<ISharedUserCommandRepository, SharedUserCommandRepository>();

        services.AddScoped<ISharedUserTokenCommandRepository, SharedUserTokenCommandRepository>();
        services.AddScoped<ISharedUserTokenQueryRepository, SharedUserTokenQueryRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddRequestTimeouts();

        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
              new QueryStringApiVersionReader("version"),
              new HeaderApiVersionReader("version"),
              new UrlSegmentApiVersionReader());
        })
        .AddApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'V";
            opt.SubstituteApiVersionInUrl = true;
        });

        services.AddCors(opt =>
        {
            opt.AddPolicy("AllowAll", p =>
            {
                p.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });

        services.AddAuthorization();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddRateLimiter(opt =>
        {
            opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            opt.AddPolicy("SignInLimit", context =>
            {
                return RateLimitPartition.GetSlidingWindowLimiter(
                    context.Connection.RemoteIpAddress, _ =>
                    {
                        return new SlidingWindowRateLimiterOptions()
                        {
                            Window = TimeSpan.FromSeconds(300),
                            SegmentsPerWindow = 3,
                            PermitLimit = 3
                        };
                    });
            });
        });


        return services;
    }
}