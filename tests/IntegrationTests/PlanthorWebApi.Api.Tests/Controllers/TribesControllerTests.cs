using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlanthorWebApi.Api.Requests;
using PlanthorWebApi.Api.Tests.TestAuthentication;
using PlanthorWebApi.Api.Tests.TestDataBuilders;
using PlanthorWebApi.Application.Dtos;
using PlanthorWebApi.Application.Tribes.Commands.Update;
using PlanthorWebApi.Infrastructure;
using Xunit;

namespace PlanthorWebApi.Api.Tests.Controllers;

public class TribesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public TribesControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Create_ValidNewTribe_ReturnsValidNewTribeIdAsync()
    {
        // Arrange
        _client.DefaultRequestHeaders.Add(TestAuthenticationHandler.TestUserRolesHeader, "Admin");
        var newTribeRequest = new CreateTribeRequest(
            "Tribe 3",
            null,
            "Test Tribe 3");

        // Act
        var response = await _client.PostAsJsonAsync("/tribes", newTribeRequest);

        // Assert
        response.EnsureSuccessStatusCode();
        var tribeId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, tribeId);
    }

    [Fact]
    public async Task Read_ValidTribeId_ReturnsValidTribeAsync()
    {
        // Arrange
        _client.DefaultRequestHeaders.Add(TestAuthenticationHandler.TestUserRolesHeader, "Admin");
        var tribe = new TribeBuilder().Build();

        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PlanthorDbContext>();

            dbContext.Tribes.Add(tribe);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
        }

        // Act
        var response = await _client.GetFromJsonAsync<TribeDto>($"/tribes/{tribe.Id}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(tribe.Id, response.Id);
        Assert.Equal(tribe.Name, response.Name);
        Assert.Equal(tribe.Description, response.Description);
    }

    [Fact]
    public async Task Update_ValidTribe_ReturnsSuccessAsync()
    {
        // Arrange
        _client.DefaultRequestHeaders.Add(TestAuthenticationHandler.TestUserRolesHeader, "Admin");
        var tribe = new TribeBuilder().Build();

        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PlanthorDbContext>();

            dbContext.Tribes.Add(tribe);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
        }

        var updateTribeCommand = new UpdateTribeCommand(
            tribe.Id,
            "Updated Tribe Name",
            "Updated Tribe Description");

        // Act
        var response = await _client.PutAsJsonAsync($"/tribes/{tribe.Id}", updateTribeCommand);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessStatusCode);
        using (var scope = scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PlanthorDbContext>();

            var result = await dbContext.Tribes.AsNoTracking().FirstAsync(x => x.Id == tribe.Id);
            Assert.Equal("Updated Tribe Name", result.Name);
            Assert.Equal("Updated Tribe Description", result.Description);
        }
    }

    [Fact]
    public async Task Delete_ValidTribeId_ReturnsSuccessAsync()
    {
        // Arrange
        _client.DefaultRequestHeaders.Add(TestAuthenticationHandler.TestUserRolesHeader, "Admin");
        var tribe = new TribeBuilder().Build();

        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PlanthorDbContext>();

            dbContext.Tribes.Add(tribe);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
        }

        // Act
        var response = await _client.DeleteAsync(new Uri($"/tribes/{tribe.Id}", UriKind.Relative));

        // Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessStatusCode);
        using (var scope = scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PlanthorDbContext>();

            var result = await dbContext.Tribes.AsNoTracking().AnyAsync(x => x.Id == tribe.Id);
            Assert.False(result);
        }
    }
}
