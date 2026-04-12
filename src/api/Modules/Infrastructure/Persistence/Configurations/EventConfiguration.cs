using Skittles.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class EventConfiguration : KeyedEntityConfiguration<Event>
{
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        base.Configure(builder);
        builder.HasOne(x => x.Season)
            .WithMany(p => p.Events)
            .HasForeignKey(x => x.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Venue)
            .WithMany(p => p.HostedEvents)
            .HasForeignKey(x => x.VenueId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Opponent)
            .WithMany(p => p.OppositionEvents)
            .HasForeignKey(x => x.OpponentId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(x => x.Description).HasMaxLength(100);
        builder.HasIndex(x => new { x.SeasonId, x.Description }).IsUnique();
    }
}