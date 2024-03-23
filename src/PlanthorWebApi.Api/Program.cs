using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlanthorWebApi.Application;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApiDocument();

    builder.Host.UseSerilog((_, config) =>
    {
        config.ReadFrom.Configuration(builder.Configuration);
    });

    builder.Services.AddMediatR(cfg =>
    {
        var mediatRAssemblies = new[]
        {
            typeof(CreateTribeCommand).Assembly
        };
        cfg.RegisterServicesFromAssemblies(mediatRAssemblies);
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        // Add OpenAPI 3.0 document serving middleware
        // Available at: http://localhost:<port>/swagger/v1/swagger.json
        app.UseOpenApi();

        // Add web UIs to interact with the document
        // Available at: http://localhost:<port>/swagger
        app.UseSwaggerUi();
    }

    app.MapControllers();

    app.Logger.LogInformation("The app started.");
    app.Run();
}
catch (InvalidOperationException ex)  // Catch specific exception
{
    // Log detailed exception information for InvalidOperationException
    Log.Error(ex, "An unexpected operation occurred.");
}
catch (AppDomainUnloadedException ex)
{
    Log.Fatal(ex, "The application domain was unloaded unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

