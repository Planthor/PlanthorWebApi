namespace PlanthorWebApi.Api.Requests;

public record CreateTribeRequest(
    string Name, 
    string? Description);