using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Tribe : IAggregateRoot, IEntity
{
    public static readonly int MaxNameLength = 128;
    public static readonly int MaxDescriptionLength = 3000;
    public static readonly int MaxNationalityLength = 2;

    public Guid Id { get; protected set; }

    public string Name { get; set; } = string.Empty;

    public string? Slogan { get; set; }

    public string? Description { get; set; }

    public string? PathAvatar { get; set; }

    public string Nationality { get; set; } = "VN";

    public IEnumerable<string> Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return "Name is required.";
        }

        if (string.IsNullOrWhiteSpace(Nationality))
        {
            yield return "Nationality is required.";
        }
    }
}
