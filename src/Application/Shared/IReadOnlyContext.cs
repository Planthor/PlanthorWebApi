using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Shared;

/// <summary>
/// Provides a generic, EF-agnostic mechanism to query domain entities for read-only operations.
/// </summary>
public interface IReadOnlyContext
{
    /// <summary>
    /// Executes a query and returns a list of projected results.
    /// </summary>
    Task<List<TResult>> QueryAsync<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
        CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Executes a query and returns the first projected result or null.
    /// </summary>
    Task<TResult?> FirstOrDefaultAsync<TEntity, TResult>(
        Func<IQueryable<TEntity>, IQueryable<TResult>> queryBuilder,
        CancellationToken cancellationToken = default) where TEntity : class;

    /// <summary>
    /// Checks if any elements satisfy the defined query.
    /// </summary>
    Task<bool> AnyAsync<TEntity>(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryBuilder,
        CancellationToken cancellationToken = default) where TEntity : class;
}
