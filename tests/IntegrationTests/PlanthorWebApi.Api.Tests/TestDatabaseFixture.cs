using Microsoft.EntityFrameworkCore;
using PlanthorWebApi.Domain;
using PlanthorWebApi.Infrastructure;

namespace PlanthorWebApi.Api.Tests;

// TODO - Trung - Investigate if this class is really needed (compared to CustomerWebApplicationFactory).
public class TestDatabaseFixture
{
    private const string ConnectionString = @"mongodb://admin:Planthor_123@localhost:27017/";

    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using (var context = CreateContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.AddRange(
                        new Tribe { Name = "Tribe 1", Description = "Test Tribe 1" },
                        new Tribe { Name = "Tribe 2", Description = "Test Tribe 2" });
                    context.SaveChanges();
                }

                _databaseInitialized = true;
            }
        }
    }

    public PlanthorDbContext CreateContext()
    => new(new DbContextOptionsBuilder<PlanthorDbContext>()
           .UseMongoDB(ConnectionString, "testplanthordb")
           .Options);
}
