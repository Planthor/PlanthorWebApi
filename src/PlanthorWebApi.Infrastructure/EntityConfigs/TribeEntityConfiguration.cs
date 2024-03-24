using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Infrastructure.EntityConfigs;

/// <summary>
/// Represents the entity configuration for the <see cref="Tribe"/> entity.
/// </summary>
internal sealed class TribeEntityConfiguration : IEntityTypeConfiguration<Tribe>
{
    /// <summary>
    /// Configures the entity mapping for the <see cref="Tribe"/> entity.
    /// </summary>
    /// <param name="builder">The entity type builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Tribe> builder)
    {
        builder.ToCollection("tribes");
    }
}
