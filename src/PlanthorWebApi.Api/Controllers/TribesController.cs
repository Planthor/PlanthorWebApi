using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanthorWebApi.Application;

namespace PlanthorWebApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TribesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender
        ?? throw new ArgumentNullException(nameof(sender));

    [HttpPost]
    public async Task<ActionResult<TribeDto>> CreateTribe(CreateTribeCommand command, CancellationToken token)
    {
        var tribeDto = await _sender.Send(command, token);
        return Ok(tribeDto);
    }
}
