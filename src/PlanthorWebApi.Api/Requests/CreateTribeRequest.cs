namespace PlanthorWebApi.Api.Requests;

/// <summary>
/// DTO for request of create new Tribe.
/// </summary>
/// <param name="Name">Name of the Tribe.</param>
/// <param name="Slogan">Slogan of the Tribe</param>
/// <param name="Description">Description of the Tribe.</param>
public record CreateTribeRequest(
    string Name,
    string? Slogan,
    string? Description);
