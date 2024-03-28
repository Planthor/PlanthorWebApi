using System;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanthorWebApi.Application;
using PlanthorWebApi.Application.Tribes.Commands.Create;
using PlanthorWebApi.Application.Tribes.Queries.Details;
using PlanthorWebApi.Domain.Shared;
using PlanthorWebApi.Infrastructure;
using PlanthorWebApi.Infrastructure.Repositories;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder(args);

    // Infrastructure
    builder.Host.UseSerilog();

    builder.Services.AddPlanthorDbContext(
        builder.Configuration.GetConnectionString("PlanthorDbContext")
            ?? throw new InvalidOperationException("PlanthorDbContext is not set in the configuration file."));

    builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(BaseWriteRepository<>));
    builder.Services.AddScoped(typeof(IReadRepository<>), typeof(BaseReadRepository<>));

    // API Client
    builder.Services.AddControllers();
    builder.Services.AddScoped<IValidator<CreateTribeCommand>, CreateTribeCommandValidator>();
    builder.Services.AddScoped<IValidator<TribeDetailsQuery>, TribeDetailsQueryValidator>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApiDocument();

    builder.Services.AddMediatR(cfg =>
    {
        var mediatRAssemblies = new[]
        {
            typeof(CreateTribeCommand).Assembly,
            typeof(TribeDetailsQuery).Assembly
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

    Log.Information("The app started.");
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
