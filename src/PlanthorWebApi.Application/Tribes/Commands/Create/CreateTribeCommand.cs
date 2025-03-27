using System;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Tribes.Commands.Create;

/// <summary>
/// Represents a command to create a new tribe.
/// </summary>
/// <param name="Name">The name of the tribe.</param>
/// <param name="Description">The description of the tribe.</param>
/// <param name="IdentityId">Identity that send the command.</param>
public record CreateTribeCommand(
    string Name, 
    string? Description,
    string IdentityId) : ICommand<Guid>;
