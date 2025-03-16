using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Goal : IAggregateRoot, IEntity
{
    public Goal()
    {
        OwnerId = new(Guid.Empty.ToString());
    }

    public Goal(Guid id, string ownerId)
    {
        OwnerId = new(ownerId);
    }

    public Guid Id { get; protected set; }

    public OwnerId OwnerId { get; protected set; }

    public IEnumerable<string> Validate()
    {
        throw new NotImplementedException();
    }
}
