using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Tribe : IAggregateRoot, IEntity
{
    public Tribe()
    {
        Name = string.Empty;
        Nationality = "VN";
        OwnerId = new(Guid.Empty.ToString());
    }

    public Tribe(
        string name,
        string? slogan,
        string? description,
        string? pathAvatar,
        string nationality,
        string ownerId)
    {
        Name = name;
        Slogan = slogan;
        Description = description;
        PathAvatar = pathAvatar;
        Nationality = nationality;
        OwnerId = new(ownerId);
    }

    public static readonly int MaxNameLength = 128;
    public static readonly int MaxDescriptionLength = 3000;
    public static readonly int MaxNationalityLength = 2;

    public Guid Id { get; protected set; }

    public string Name { get; set; }

    public string? Slogan { get; set; }

    public string? Description { get; set; }

    public string? PathAvatar { get; set; }

    public string Nationality { get; protected set; }

    public OwnerId OwnerId { get; protected set; }

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
