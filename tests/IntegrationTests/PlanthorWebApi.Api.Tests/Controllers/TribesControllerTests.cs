using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http.Json;
using PlanthorWebApi.Application;
using System;

namespace PlanthorWebApi.Api.Tests.Controllers;

public class TribesControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Create_ValidNewTribe_ReturnsValidNewTribeIdAsync()
    {
        // Arrange
        var client = factory.CreateClient();
        var newTribeCommand = new CreateTribeCommand("Tribe 3", "Test Tribe 3");


        var response = "test";
        // Act
        try
        {
            var responseTest = await client.PostAsJsonAsync("/tribes", newTribeCommand);
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Error in Create_ValidNewTribe_ReturnsValidNewTribeIdAsync");
            throw;
        }

        // Assert
        // response.EnsureSuccessStatusCode();
        // var tribeId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, Guid.Empty);
    }
}
