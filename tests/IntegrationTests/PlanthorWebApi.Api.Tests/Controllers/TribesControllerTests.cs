using Xunit;
using System.Threading.Tasks;
using System.Net.Http.Json;
using PlanthorWebApi.Application;
using System;

namespace PlanthorWebApi.Api.Tests.Controllers;

public class TribesControllerTests(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
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
}
