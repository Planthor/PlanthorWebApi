using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PlanthorWebApi.Api.Tests.TestDataBuilders;
using PlanthorWebApi.Application;
using PlanthorWebApi.Infrastructure;
using Xunit;

namespace PlanthorWebApi.Api.Tests.Controllers;

public class TribesControllerTests(CustomWebApplicationFactory<Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    [Fact]
    public async Task Create_ValidNewTribe_ReturnsValidNewTribeIdAsync()
    {
        // Arrange
        var client = factory.CreateClient();
        var newTribeCommand = new CreateTribeCommand("Tribe 3", "Test Tribe 3");

        // Act
        var response = await client.PostAsJsonAsync("/tribes", newTribeCommand);

        // Assert
        response.EnsureSuccessStatusCode();
        var tribeId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, tribeId);
    }

    [Fact]
    public async Task Read_ValidTribeId_ReturnsValidTribeAsync()
    {
        // Arrange
        var tribe = new TribeBuilder().Build();

        var client = factory.CreateClient();
        var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PlanthorDbContext>();

            dbContext.Tribes.Add(tribe);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
        }

        // Act
        var response = await client.GetFromJsonAsync<TribeDto>($"/tribes/{tribe.Id}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(tribe.Id, response.Id);
        Assert.Equal(tribe.Name, response.Name);
        Assert.Equal(tribe.Description, response.Description);
    }
}
