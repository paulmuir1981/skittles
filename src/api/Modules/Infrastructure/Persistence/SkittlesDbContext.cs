using Microsoft.EntityFrameworkCore;
using Skittles.Framework.Infrastructure.Persistence;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Infrastructure.Persistence;

public sealed class SkittlesDbContext: DbContext
{
    public SkittlesDbContext(DbContextOptions<SkittlesDbContext> options) : base(options)
    {
    }

    public DbSet<Player> Players { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkittlesDbContext).Assembly);
        
        // Only apply schema for SQL Server (SQLite doesn't support schemas)
        if (Database.IsSqlServer())
        {
            modelBuilder.HasDefaultSchema(SchemaNames.Skittles);
        }
        else if (Database.IsSqlite())
        {
            // Remove schema from all entities for SQLite
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetSchema(null);
            }
        }
    }
}
