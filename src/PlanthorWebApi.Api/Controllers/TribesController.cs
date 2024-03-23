using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanthorWebApi.Application;

namespace PlanthorWebApi.Api.Controllers;

/// <summary>
/// Controller for handling requests related to Tribes.
/// </summary>
/// <param name="sender">The sender used to send requests.</param>
[ApiController]
[Route("[controller]")]
public class TribesController(ISender sender) : ControllerBase
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
    /// The task result contains the ActionResult of <see cref="TribeDto"/>.
    /// </returns>
    [HttpPost]
    public async Task<ActionResult<TribeDto>> Create(CreateTribeCommand command, CancellationToken token)
    {
        var tribeDto = await _sender.Send(command, token);
        return Ok(tribeDto);
    }
}
