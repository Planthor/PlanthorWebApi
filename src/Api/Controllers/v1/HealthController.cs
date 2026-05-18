using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.v1;

/// <summary>
/// Public health check endpoint for verifying API connectivity and readiness.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class HealthController : ControllerBase
{
    private readonly PlanthorDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthController"/> class.
    /// </summary>
    /// <param name="dbContext">The Planthor database context.</param>
    public HealthController(PlanthorDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>
    /// Returns the current health status of the API.
    /// </summary>
    /// <returns>A JSON object containing status, timestamp, and version.</returns>
    /// <response code="200">The API is healthy and responding.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "healthy",
            timestamp = DateTimeOffset.UtcNow.ToString("O"),
            version = "1.0"
        });
    }

    /// <summary>
    /// Checks MongoDB connectivity by executing a database ping operation.
    /// </summary>
    /// <returns>A JSON object containing MongoDB connection status, latency, and error details if applicable.</returns>
    /// <response code="200">MongoDB is connected and responding.</response>
    /// <response code="503">MongoDB is unavailable or connection failed.</response>
    [HttpGet("mongodb")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CheckMongoDBHealthAsync()
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            // Attempt to connect and execute a simple query
            await _dbContext.Members.AsNoTracking().CountAsync();
            stopwatch.Stop();

            return Ok(new
            {
                status = "connected",
                database = "planthordb",
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                message = "MongoDB connection successful"
            });
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "disconnected",
                database = "planthordb",
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                error = ex.Message,
                errorType = ex.GetType().Name
            });
        }
    }
}
