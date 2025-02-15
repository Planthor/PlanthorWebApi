using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Goal : IAggregateRoot, IEntity
{
    protected Goal()
    { }

    public Guid Id => throw new NotImplementedException();

    public IEnumerable<string> Validate()
    {
        throw new NotImplementedException();
    }
}
