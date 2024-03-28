using System;

namespace PlanthorWebApi.Domain.Shared;

/// <summary>
/// Exception that is thrown when a specific entity is not found.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public class EntityNotFoundException<TEntity> : Exception where TEntity : IEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity}"/> class.
    /// </summary>
    public EntityNotFoundException()
        : base($"Entity of type {typeof(TEntity).Name} was not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityNotFoundException{TEntity}"/> class.
    /// </summary>
    /// <param name="id">The identifier of the entity that was not found.</param>
    public EntityNotFoundException(Guid id)
        : base($"Entity of type {typeof(TEntity).Name} with ID {id} was not found.")
    {
    }
}
