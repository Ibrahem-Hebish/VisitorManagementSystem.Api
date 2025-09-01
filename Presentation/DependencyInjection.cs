using Domain.TenantDomain.Branches.Repositories;
using Domain.TenantDomain.Buildings.Repositories;
using Domain.TenantDomain.EntryLogs.Repositories;
using Domain.TenantDomain.Permits.Repositories;
using Domain.TenantDomain.PermitUpdateRequests.Repositories;
using Domain.TenantDomain.Roles.Repositories;
using Domain.TenantDomain.Tenants.Repositories;
using Domain.TenantDomain.Tokens.Repositories;
using Domain.TenantDomain.Users.Repositories.Employees;
using Domain.TenantDomain.Users.Repositories.Managers;
using Domain.TenantDomain.Users.Repositories.Requesters;
using Domain.TenantDomain.Users.Repositories.Securities;
using Domain.TenantDomain.Users.Repositories.Users;
using Domain.TenantDomain.Visitors.Repositories;

namespace Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add<TenantFilter>();
        });

        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

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

        services.AddTransient<IPdfTranslationService, PdfTranslationService>();

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

        services.AddScoped<IBuildingQueryRepository, BuildingQueryRepository>();
        services.AddScoped<IBuildingCommandRepository, BuildingCommandRepository>();

        services.AddScoped<IVisitorQueryRepository, VisitorQueryRepository>();
        services.AddScoped<IVisitorCommandRepository, VisitorCommandRepository>();

        services.AddScoped<IEntryLogCommandRepository, EntryLogCommandRepository>();

        services.AddScoped<IPermitUpdateRequestQueryRepository, PermitUpdateRequestQueryRepository>();

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