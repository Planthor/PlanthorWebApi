using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanthorWebApi.Application;
using PlanthorWebApi.Application.Tribes.Queries.Details;

namespace PlanthorWebApi.Api.Controllers;

/// <summary>
/// Controller for handling requests related to Tribes.
/// </summary>
/// <param name="sender">The sender used to send requests.</param>
/// <param name="createTribeCommandValidator">The validator used to validate the <see cref="CreateTribeCommand"/>.</param>
/// <param name="tribeDetailsQueryValidator">The validator used to validate the <see cref="TribeDetailsQuery"/>.</param>
[ApiController]
[Route("[controller]")]
public class TribesController(
    ISender sender,
    IValidator<CreateTribeCommand> createTribeCommandValidator,
    IValidator<TribeDetailsQuery> tribeDetailsQueryValidator)
    : ControllerBase
{
    private readonly ISender _sender = sender
        ?? throw new ArgumentNullException(nameof(sender));

    /// <summary>
    /// Creates a new tribe.
    /// </summary>
    /// <param name="command">The command to create a new tribe.</param>
    /// <param name="token">A cancellation token that can be used to cancel the work.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the ActionResult of <see cref="Guid"/>.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateTribeCommand command, CancellationToken token)
    {
        await createTribeCommandValidator.ValidateAndThrowAsync(command, token);
        var newTribeGuid = await _sender.Send(command, token);
        return Ok(newTribeGuid);
    }

    /// <summary>
    /// Reads the details of a tribe.
    /// </summary>
    /// <param name="id">The identifier of the tribe.</param>
    /// <param name="token">A cancellation token that can be used to cancel the work.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the ActionResult of <see cref="TribeDto"/>.
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TribeDto>> Read(Guid id, CancellationToken token)
    {
        var query = new TribeDetailsQuery(id);
        await tribeDetailsQueryValidator.ValidateAndThrowAsync(query, token);
        var tribeDto = await _sender.Send(query, token);
        return Ok(tribeDto);
    }
}
