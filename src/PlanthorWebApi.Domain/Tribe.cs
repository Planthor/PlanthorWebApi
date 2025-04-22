using System;
using System.Collections.Generic;
using System.Linq;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

/// <summary>
/// </summary>
public class Tribe : IAggregateRoot, IEntity, IOwnedEntity
{
    public Tribe(
        string name,
        string? slogan,
        string? description,
        string ownerId
    )
    {
        Name = name;
        Slogan = slogan;
        Description = description;
        OwnerId = ownerId;
        _memberships = []; // TODO : initialize the membership with Leader from DDD RaiseEvent, so we can have IClock implementation in EventHandler
    }

    public static readonly int MaxNameLength = 128;
    public static readonly int MaxDescriptionLength = 3000;
    public static readonly int MaxNationalityLength = 2;

    public Guid Id { get; protected set; }

    public string Name { get; set; }

    public string? Slogan { get; set; }

    public string? Description { get; set; }

    public string OwnerId { get; protected set; }

    private readonly List<TribeMember> _memberships;
    public IReadOnlyCollection<TribeMember> Memberships => _memberships.AsReadOnly();

    public void AddMember(Guid memberId, IClock clock, bool isLeader)
    {
        if (clock is null)
        {
            throw new ArgumentNullException(nameof(clock));
        }

        if (_memberships.Any(m => m.MemberId == memberId))
        {
            return;
        }

        var tribeMember = new TribeMember(Guid.NewGuid(), clock.UtcNow, isLeader);
        _memberships.Add(tribeMember);
    }

    public IEnumerable<string> Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return "Name is required.";
        }
    }
}
