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
        modelBuilder.HasDefaultSchema(SchemaNames.Skittles);
    }
}
