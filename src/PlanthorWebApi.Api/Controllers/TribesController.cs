using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlanthorWebApi.Application;

namespace PlanthorWebApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TribesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    public async Task<ActionResult<TribeDto>> CreateTribe(CreateTribeCommand command, CancellationToken token)
    {
        var tribeDto = await _mediator.Send(command, token);
        return Ok(tribeDto);
    }
}
