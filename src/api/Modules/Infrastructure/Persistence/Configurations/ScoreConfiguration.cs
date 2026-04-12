using Skittles.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class ScoreConfiguration : IEntityTypeConfiguration<Score>
{
    public void Configure(EntityTypeBuilder<Score> builder)
    {
        builder.HasKey(x => new { x.PlayerId, x.LegId });
        builder.HasOne(x => x.Player)
            .WithMany(p => p.Scores)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Leg)
            .WithMany(w => w.Scores)
            .HasForeignKey(x => x.LegId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}