using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Tribe : IAggregateRoot, IEntity
{
    public static readonly int MaxNameLength = 128;
    public static readonly int MaxDescriptionLength = 3000;

    public Guid Id { get; protected set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public IEnumerable<string> Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return "Name is required.";
        }
    }
}
