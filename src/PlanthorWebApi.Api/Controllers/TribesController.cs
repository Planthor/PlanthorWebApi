using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanthorWebApi.Application;
using PlanthorWebApi.Application.Tribes.Commands.Create;
using PlanthorWebApi.Application.Tribes.Commands.Update;
using PlanthorWebApi.Application.Tribes.Queries.Details;

namespace PlanthorWebApi.Api.Controllers;

/// <summary>
/// Controller for handling requests related to Tribes.
/// </summary>
/// <param name="sender">The sender used to send requests.</param>
/// <param name="createTribeCommandValidator">The validator used to validate the <see cref="CreateTribeCommand"/>.</param>
/// <param name="tribeDetailsQueryValidator">The validator used to validate the <see cref="TribeDetailsQuery"/>.</param>
/// <param name="updateTribeCommandValidator">The validator used to validate the <see cref="UpdateTribeCommand"/>.</param>
[ApiController]
[Route("[controller]")]
public class TribesController(
    ISender sender,
    IValidator<CreateTribeCommand> createTribeCommandValidator,
    IValidator<TribeDetailsQuery> tribeDetailsQueryValidator,
    IValidator<UpdateTribeCommand> updateTribeCommandValidator)
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

    /// <summary>
    /// Updates a tribe based on the provided information.
    /// </summary>
    /// <param name="id">The unique identifier of the tribe to update.</param>
    /// <param name="command">An object containing the update details for the tribe. (See UpdateTribeCommand class for details)</param>
    /// <param name="token">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>An empty `204 No Content` response if successful, otherwise an appropriate error response.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateTribeCommand command,
        CancellationToken token)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var updateTribeCommand = command with { Id = id };
        await updateTribeCommandValidator.ValidateAndThrowAsync(updateTribeCommand, token);
        await _sender.Send(updateTribeCommand, token);
        return NoContent();
    }
}
