WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddUserSecrets<Program>(true);

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddInfrustructure(builder.Configuration);

builder.Services.AddWeb(builder.Configuration);

var app = builder.Build();

await app.ConfigureAsync(builder.Configuration);

app.Run();
