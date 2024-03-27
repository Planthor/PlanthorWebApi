using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlanthorWebApi.Domain.Shared;

/// <summary>
/// Represents a generic repository interface for managing aggregate roots.
/// </summary>
/// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
public interface IWriteRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
{
    /// <summary>
    /// Adds a new aggregate root asynchronously.
    /// </summary>
    /// <param name="item">The aggregate root to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<TAggregateRoot> AddAsync(TAggregateRoot item, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a range of aggregate roots asynchronously.
    /// </summary>
    /// <param name="items">The range of aggregate roots to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<IEnumerable<TAggregateRoot>> AddRangeAsync(IEnumerable<TAggregateRoot> items, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing aggregate root asynchronously.
    /// </summary>
    /// <param name="item">The aggregate root to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(TAggregateRoot item, CancellationToken cancellationToken);

    /// <summary>
    /// Updates a range of existing aggregate roots asynchronously.
    /// </summary>
    /// <param name="items">The range of aggregate roots to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateRangeAsync(IEnumerable<TAggregateRoot> items, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an existing aggregate root asynchronously.
    /// </summary>
    /// <param name="item">The aggregate root to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(TAggregateRoot item, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a range of existing aggregate roots asynchronously.
    /// </summary>
    /// <param name="items">The range of aggregate roots to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteRangeAsync(IEnumerable<TAggregateRoot> items, CancellationToken cancellationToken);

    /// <summary>
    /// Saves changes made to the repository asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
