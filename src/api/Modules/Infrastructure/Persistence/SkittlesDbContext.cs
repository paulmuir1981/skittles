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
        
        // Check the provider name from the options instead of Database connection
        var provider = Database.ProviderName;
        
        if (provider == "Microsoft.EntityFrameworkCore.SqlServer")
        {
            modelBuilder.HasDefaultSchema(SchemaNames.Skittles);
        }
        else if (provider == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            // Remove schema from all entities for SQLite
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetSchema(null);
            }
        }
    }
}
