using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlanthorWebApi.Application.Shared;
using PlanthorWebApi.Domain;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Application.Tribes.Commands.Delete;

internal sealed class DeleteTribeCommandHandler(
    ILogger<DeleteTribeCommandHandler> logger,
    IReadRepository<Tribe> existingTribeRepository,
    IWriteRepository<Tribe> tribeWriteRepository) : ICommandHandler<DeleteTribeCommand>
{
    public async Task Handle(DeleteTribeCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("DeleteTribeCommand - Handle - Start");
        var existingTribe = await existingTribeRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<Tribe>();

        await tribeWriteRepository.DeleteAsync(existingTribe, cancellationToken);
    }
}
