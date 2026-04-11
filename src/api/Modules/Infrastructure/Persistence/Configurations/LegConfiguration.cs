using Skittles.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class LegConfiguration : IEntityTypeConfiguration<Leg>
{
    public void Configure(EntityTypeBuilder<Leg> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Event)
            .WithMany(p => p.Legs)
            .HasForeignKey(x => x.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}