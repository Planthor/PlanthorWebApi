using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

/// <summary>
/// TODO: Verify if EF Core can understand with ID and without ID in constructor. Impact Id generation solution.
/// </summary>
/// <param name="name"></param>
/// <param name="slogan"></param>
/// <param name="description"></param>
/// <param name="ownerId"></param>
public class Tribe(
    string name,
    string? slogan,
    string? description,
    string ownerId) : IAggregateRoot, IEntity, IOwnedEntity
{
    public static readonly int MaxNameLength = 128;
    public static readonly int MaxDescriptionLength = 3000;
    public static readonly int MaxNationalityLength = 2;

    public Guid Id { get; protected set; }

    public string Name { get; set; } = name;

    public string? Slogan { get; set; } = slogan;

    public string? Description { get; set; } = description;

    public string OwnerId { get; protected set; } = ownerId;

    public IEnumerable<string> Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return "Name is required.";
        }
    }
}
