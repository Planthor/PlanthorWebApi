using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PlanthorWebApi.Domain.Shared;

namespace PlanthorWebApi.Infrastructure;

public sealed class BaseReadRepository<TEntity>(PlanthorDbContext dbContext)
    : IReadRepository<TEntity> where TEntity : IEntity
{
    /// <inheritdoc/>
    public Task<IList<TEntity>> GetAllAsync(Func<TEntity, bool>? filter, CancellationToken cancellationToken)
    {
        // TODO(Trung): Implement GetAllAsync following MongoDB EF Core driver.
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO(Trung): Implement GetByIdAsync following MongoDB EF Core driver.
        throw new NotImplementedException();
    }
}
