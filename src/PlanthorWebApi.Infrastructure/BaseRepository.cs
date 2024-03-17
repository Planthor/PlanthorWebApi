using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Infrastructure;

/// <summary>
/// Represents a base repository implementation for a specific aggregate root type.
/// </summary>
/// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
/// <param name="dbContext">The DbContext instance to use for database access.</param>
public class BaseRepository<TAggregateRoot>(DbContext dbContext) : IRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{
    private readonly DbContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Adds a new item to the repository asynchronously.
    /// </summary>
    /// <param name="item">The item to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added item.</returns>
    public async Task<TAggregateRoot> AddAsync(TAggregateRoot item, CancellationToken cancellationToken)
    {
        _dbContext.Set<TAggregateRoot>().Add(item);
        await SaveChangesAsync(cancellationToken);
        return item;
    }

    /// <summary>
    /// Adds a range of items to the repository asynchronously.
    /// </summary>
    /// <param name="items">The items to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The added items.</returns>
    public async Task<IEnumerable<TAggregateRoot>> AddRangeAsync(IEnumerable<TAggregateRoot> items, CancellationToken cancellationToken)
    {
        _dbContext.Set<TAggregateRoot>().AddRange(items);
        await SaveChangesAsync(cancellationToken);
        return items;
    }

    /// <summary>
    /// Deletes an item from the repository asynchronously.
    /// </summary>
    /// <param name="item">The item to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task DeleteAsync(TAggregateRoot item, CancellationToken cancellationToken)
    {
        _dbContext.Set<TAggregateRoot>().Remove(item);
        await SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a range of items from the repository asynchronously.
    /// </summary>
    /// <param name="items">The items to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task DeleteRangeAsync(IEnumerable<TAggregateRoot> items, CancellationToken cancellationToken)
    {
        _dbContext.Set<TAggregateRoot>().RemoveRange(items);
        await SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Saves the changes made to the repository asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of affected entities.</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an item in the repository asynchronously.
    /// </summary>
    /// <param name="item">The item to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task UpdateAsync(TAggregateRoot item, CancellationToken cancellationToken)
    {
        _dbContext.Set<TAggregateRoot>().Update(item);
        await SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates a range of items in the repository asynchronously.
    /// </summary>
    /// <param name="items">The items to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task UpdateRangeAsync(IEnumerable<TAggregateRoot> items, CancellationToken cancellationToken)
    {
        _dbContext.Set<TAggregateRoot>().UpdateRange(items);
        await SaveChangesAsync(cancellationToken);
    }
}
