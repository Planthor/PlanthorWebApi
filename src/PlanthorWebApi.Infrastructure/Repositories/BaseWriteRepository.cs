using System;
using System.Threading;
using System.Threading.Tasks;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Infrastructure.Repositories;

/// <summary>
/// Represents a base repository implementation for a specific aggregate root type.
/// </summary>
/// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
/// <param name="dbContext">The PlanthorDbContext instance to use for database access.</param>
public sealed class BaseWriteRepository<TAggregateRoot>(PlanthorDbContext dbContext) : IWriteRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{
    private readonly PlanthorDbContext _dbContext = dbContext
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
}
