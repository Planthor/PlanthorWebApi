using System;
using System.Text.Json.Serialization;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Tribes.Commands.Delete;

public record DeleteTribeCommand([property: JsonIgnore] Guid Id) : ICommand;
