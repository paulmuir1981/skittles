using Skittles.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Season)
            .WithMany(p => p.Events)
            .HasForeignKey(x => x.SeasonId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(x => x.Opponent).HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(100);
    }
}