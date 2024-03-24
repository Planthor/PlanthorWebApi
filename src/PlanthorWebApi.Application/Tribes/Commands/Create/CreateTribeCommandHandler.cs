using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlanthorWebApi.Application.Shared;
using PlanthorWebApi.Domain;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Application.Tribes.Commands.Create;

/// <summary>
/// Handles the creation of a new <see cref="Tribe"/>.
/// </summary>
/// <param name="logger">The logger used to log information.</param>
/// <param name="tribes">The repository of tribes.</param>
internal sealed class CreateTribeCommandHandler(
    ILogger<CreateTribeCommandHandler> logger,
    IRepository<Tribe> tribes)
    : ICommandHandler<CreateTribeCommand, Guid>
{
    /// <inheritdoc/>
    public async Task<Guid> Handle(CreateTribeCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateTribeCommandHandler - Handle - Start");

        var newTribe = new Tribe
        {
            Name = request.Name,
            Description = request.Description
        };

        var result = await tribes.AddAsync(newTribe, cancellationToken);

        logger.LogInformation("new TribeId: {TribeId}", result.Id);

        return result.Id;
    }
}
