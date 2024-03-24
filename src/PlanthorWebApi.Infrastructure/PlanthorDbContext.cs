using Microsoft.EntityFrameworkCore;
using PlanthorWebApi.Domain;

namespace PlanthorWebApi.Infrastructure;

class PlanthorDbContext : DbContext
{
    public DbSet<Tribe> Movies { get; init; }
}
