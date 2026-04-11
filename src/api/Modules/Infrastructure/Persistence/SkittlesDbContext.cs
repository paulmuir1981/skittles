using Microsoft.EntityFrameworkCore;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Infrastructure.Persistence;

public sealed class SkittlesDbContext: DbContext
{
    public SkittlesDbContext(DbContextOptions<SkittlesDbContext> options) : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Leg> Legs { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Score> Scores { get; set; } = null!;
    public DbSet<Season> Seasons { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SkittlesDbContext).Assembly);
    }
}
