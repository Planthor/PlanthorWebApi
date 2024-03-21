using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlanthorWebApi.Application.Shared;

namespace PlanthorWebApi.Application.Tribes.Commands.Create;

/// <summary>
/// Handles the creation of a new <see cref="Tribe"/>.
/// </summary>
/// <param name="logger">The logger used to log information.</param>
internal sealed class CreateTribeCommandHandler(ILogger<CreateTribeCommandHandler> logger)
    : ICommandHandler<CreateTribeCommand, TribeDto>
{
    /// <inheritdoc/>
    public Task<TribeDto> Handle(CreateTribeCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("CreateTribeCommandHandler - Handle - Start");
        var tribeDto = new TribeDto(
            Guid.NewGuid(),
            request.Name,
            request.Description);

        logger.LogInformation("TribeDto: {@TribeDto}", tribeDto);
        return Task.FromResult(tribeDto);
    }
}
