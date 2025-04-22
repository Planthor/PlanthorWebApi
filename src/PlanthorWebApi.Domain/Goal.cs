using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Goal(Guid id) : IAggregateRoot, IEntity
{
    public Guid Id { get; protected set; } = id;

    public IEnumerable<string> Validate()
    {
        throw new NotImplementedException();
    }
}
