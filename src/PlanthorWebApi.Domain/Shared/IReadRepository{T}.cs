using System;
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
}
