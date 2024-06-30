using System;
using System.Collections.Generic;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Domain;

public class Member : IAggregateRoot, IEntity
{
    public Guid Id => throw new NotImplementedException();

    public IEnumerable<string> Validate()
    {
        throw new NotImplementedException();
    }
}
