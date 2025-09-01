namespace Infrustructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrustructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<EmailSettings>(configuration.GetSection("Email"));

        services.AddHttpContextAccessor();

        services.Configure<JwtOptions>(configuration.GetSection("jwt"));

        var jwt = configuration.GetSection("jwt").Get<JwtOptions>();

        var signingKey = configuration["jwtsigningkey"];

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.SaveToken = true;

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt!.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwt!.Audience,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                               Encoding.UTF8.GetBytes(signingKey!)),
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Cookies["access-token"];

                            context.Token = accessToken;

                            return Task.CompletedTask;
                        },
                    };
                });
        //.AddCookie(opt =>
        //{
        //    opt.Cookie.HttpOnly = true;
        //    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        //    opt.Cookie.SameSite = SameSiteMode.Strict;
        //    opt.Cookie.Name = "access-token";
        //});

        return services;
    }
}
