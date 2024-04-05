using System;
using System.Text.Json.Serialization;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Tribes.Commands.Update;

public record UpdateTribeCommand(
    [property: JsonIgnore] Guid Id,
    string Name,
    string? Description)
    : ICommand;
