using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlanthorWebApi.Domain.Shared;

/// <summary>
/// Defines a read-only repository for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IReadRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <param name="cancellationToken">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity, or null if not found.</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all entities that match the specified filter.
    /// </summary>
    /// <param name="filter">A function to test each entity for a condition. Null to get all entities.</param>
    /// <param name="cancellationToken">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities that match the filter.</returns>
    Task<IList<TEntity>> GetAllAsync(Func<TEntity, bool>? filter, CancellationToken cancellationToken);
}
