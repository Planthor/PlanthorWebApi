using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlanthorWebApi.Application.Shared;
using PlanthorWebApi.Domain;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Application.Tribes.Commands.Update;

internal sealed class UpdateTribeCommandHandler(
    ILogger<UpdateTribeCommandHandler> logger,
    IReadRepository<Tribe> existingTribeRepository,
    IWriteRepository<Tribe> tribeWriteRepository) : ICommandHandler<UpdateTribeCommand>
{
    public async Task Handle(UpdateTribeCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateTribeCommand - Handle - Start");
        var existingTribe = await existingTribeRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<Tribe>();

        existingTribe.Description = request.Description;
        existingTribe.Name = request.Name;

        await tribeWriteRepository.UpdateAsync(existingTribe, cancellationToken);
    }
}
