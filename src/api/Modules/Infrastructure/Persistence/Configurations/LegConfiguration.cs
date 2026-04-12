using Skittles.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class LegConfiguration : KeyedEntityConfiguration<Leg>
{
    public override void Configure(EntityTypeBuilder<Leg> builder)
    {
        base.Configure(builder);
        builder.HasOne(x => x.Event)
            .WithMany(p => p.Legs)
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.EventId, x.Number }).IsUnique();
    }
}