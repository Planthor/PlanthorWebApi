using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Infrastructure.Repositories;

/// <summary>
/// Provides a base implementation of the <see cref="IReadRepository{TEntity}"/> interface.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public sealed class BaseReadRepository<TEntity>(PlanthorDbContext dbContext)
    : IReadRepository<TEntity> where TEntity : class, IEntity
{
    private readonly PlanthorDbContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
