using System;

namespace PlanthorWebApi.Domain

{
    /// <summary>
    /// Represents an aggregate root in the domain model.
    /// </summary>
    /// <remarks>
    /// In Domain-Driven Design (DDD), an aggregate root is an entity that acts as a boundary for a group of related entities.
    /// It is responsible for maintaining the consistency and integrity of the entities within the aggregate.
    /// </remarks>
    public interface IAggregateRoot { }
}
