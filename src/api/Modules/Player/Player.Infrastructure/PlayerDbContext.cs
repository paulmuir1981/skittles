using Microsoft.EntityFrameworkCore;
using Skittles.WebApi.Player.Domain;

namespace Skittles.WebApi.Player.Infrastructure;

public class PlayerDbContext : DbContext
{
    public PlayerDbContext(DbContextOptions<PlayerDbContext> options) : base(options)
    {
    }

    public DbSet<Skittles.WebApi.Player.Domain.Player> Players { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Skittles.WebApi.Player.Domain.Player>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(p => p.Created)
                .IsRequired();
            
            entity.Property(p => p.CreatedBy)
                .IsRequired();
            
            entity.Property(p => p.LastModified)
                .IsRequired();
            
            entity.Property(p => p.LastModifiedBy);
        });
    }
}
