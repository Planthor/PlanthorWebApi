using System;

namespace PlanthorWebApi.Domain.Shared;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}
