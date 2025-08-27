using Application.Services.PasswordHashing;
using Persistence.TenantDb.Seeders;
using Presentation.Middlewares;

namespace Presentation;

public static class AppExtention
{
    public static async Task ConfigureAsync(this WebApplication app, IConfiguration configuration)
    {
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;

            var dbContext = serviceProvider.GetRequiredService<TenantDbContext>();
            var tenantService = serviceProvider.GetRequiredService<ITenantService>();
            var iConfiguration = serviceProvider.GetRequiredService<IConfiguration>();

            var passwordHashingService = serviceProvider.GetRequiredService<IPasswordHashingService>();

            await Seeder.SeedRoles(dbContext, tenantService, iConfiguration);

            await Seeder.SeedAdmin(dbContext, passwordHashingService, tenantService, configuration);
        }


        if (app.Environment.IsDevelopment())
        {

        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseMiddleware<ExeptionHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseRouting();

        app.UseRateLimiter();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseRequestTimeouts();

        app.MapControllers();
    }
}
