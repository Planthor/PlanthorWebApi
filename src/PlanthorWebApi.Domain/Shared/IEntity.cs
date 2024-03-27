using System;

namespace PlanthorWebApi.Domain.Shared;

/// <summary>
/// Represents an entity with an identity in the Planthor domain.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the unique identifier for this entity.
    /// </summary>
    Guid Id { get; }
}
