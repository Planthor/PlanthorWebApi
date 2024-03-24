using System;

namespace PlanthorWebApi.Application;

/// <summary>
/// Data Transfer Object for the Tribe entity.
/// </summary>
/// <param name="Id">Gets the unique identifier for the Tribe.</param>
/// <param name="Name">Gets the name of the Tribe.</param>
/// <param name="Description">Gets the description of the Tribe.</param>
public record TribeDto(Guid Id, string Name, string? Description);
