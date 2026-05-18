using System;
using System.Data.Common;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers.v1;

/// <summary>
/// Public health check endpoint for verifying API connectivity and readiness.
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public class HealthController : ControllerBase
{
    private readonly PlanthorDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="HealthController"/> class.
    /// </summary>
    /// <param name="dbContext">The Planthor database context.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <param name="httpClientFactory">The HTTP client factory for making requests.</param>
    public HealthController(PlanthorDbContext dbContext, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
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
        catch (DbException)
        {
            stopwatch.Stop();
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "disconnected",
                database = "planthordb",
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                error = "Unable to connect to database",
                errorType = "DatabaseConnectionException"
            });
        }
    }

    /// <summary>
    /// Checks authentication server (Keycloak) connectivity.
    /// </summary>
    /// <returns>A JSON object containing authentication server connection status, latency, and error details if applicable.</returns>
    /// <response code="200">Authentication server is reachable and responding.</response>
    /// <response code="503">Authentication server is unavailable or unreachable.</response>
    [HttpGet("auth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CheckAuthHealthAsync()
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var authority = _configuration["Authentication:Keycloak:Authority"]
                ?? _configuration["Authentication:Authority"]
                ?? "http://localhost:8180/realms/planthor";

            // Construct the well-known OpenID Connect discovery endpoint
            var discoveryUrl = authority.TrimEnd('/') + "/.well-known/openid-configuration";

            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(10);

            using var response = await client.GetAsync(discoveryUrl);
            stopwatch.Stop();

            if (response.IsSuccessStatusCode)
            {
                return Ok(new
                {
                    status = "connected",
                    server = "keycloak",
                    authority = authority,
                    latencyMs = stopwatch.ElapsedMilliseconds,
                    timestamp = DateTimeOffset.UtcNow.ToString("O"),
                    message = "Authentication server is reachable"
                });
            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "disconnected",
                server = "keycloak",
                authority = authority,
                httpStatusCode = (int)response.StatusCode,
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                error = $"Server returned {response.StatusCode}"
            });
        }
        catch (HttpRequestException)
        {
            stopwatch.Stop();
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "disconnected",
                server = "keycloak",
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                error = "Connection failed",
                errorType = "HttpRequestException"
            });
        }
        catch (OperationCanceledException)
        {
            stopwatch.Stop();
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "disconnected",
                server = "keycloak",
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                error = "Connection timeout",
                errorType = "OperationCanceledException"
            });
        }
        catch (InvalidOperationException)
        {
            stopwatch.Stop();
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "disconnected",
                server = "keycloak",
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                error = "Unable to verify connection",
                errorType = "InvalidOperationException"
            });
        }
        catch (UriFormatException)
        {
            stopwatch.Stop();
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new
            {
                status = "disconnected",
                server = "keycloak",
                latencyMs = stopwatch.ElapsedMilliseconds,
                timestamp = DateTimeOffset.UtcNow.ToString("O"),
                error = "Unable to verify connection",
                errorType = "UriFormatException"
            });
        }
    }
}
