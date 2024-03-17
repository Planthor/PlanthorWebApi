using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application;

public record CreateTribeCommand(string Name, string Description) : ICommand<TribeDto>;
