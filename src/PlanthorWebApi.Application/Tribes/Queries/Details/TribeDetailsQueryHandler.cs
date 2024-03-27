using MediatR;
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
public class TribeDetailsQueryHandler(IReadRepository<Tribe> tribes)
    : IRequestHandler<TribeDetailsQuery, TribeDto>
{
    private readonly IReadRepository<Tribe> _tribes = tribes
        ?? throw new ArgumentNullException(nameof(tribes));

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
        var tribeDetails = await _tribes.GetByIdAsync(request.TribeId, cancellationToken)
            ?? throw new EntityNotFoundException<Tribe>(request.TribeId);

        return new TribeDto(
            tribeDetails.Id,
            tribeDetails.Name,
            tribeDetails.Description);
    }
}
