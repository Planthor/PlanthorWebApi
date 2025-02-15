using MediatR;
using Microsoft.Extensions.Logging;
using PlanthorWebApi.Application.Dtos;
using PlanthorWebApi.Domain;
using PlanthorWebApi.Domain.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlanthorWebApi.Application.Tribes.Queries.Details;

/// <summary>
/// Handles the <see cref="TribeDetailsQuery"/> to get the details of a tribe.
/// </summary>
/// <param name="tribes">The repository of tribes.</param>
public class TribeDetailsQueryHandler(
    ILogger<TribeDetailsQueryHandler> logger,
    IReadRepository<Tribe> tribes)
    : IRequestHandler<TribeDetailsQuery, TribeDto>
{
    /// <inheritdoc/>
    public Task<TribeDto> Handle(
        TribeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        return HandleAsync(request, cancellationToken);
    }

    private async Task<TribeDto> HandleAsync(
        TribeDetailsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("TribeDetailsQuery - Handle - Start");

        var tribeDetails = await tribes.GetByIdAsync(request.TribeId, cancellationToken)
            ?? throw new EntityNotFoundException<Tribe>(request.TribeId);

        return new TribeDto(
            tribeDetails.Id,
            tribeDetails.Name,
            tribeDetails.Description);
    }
}
