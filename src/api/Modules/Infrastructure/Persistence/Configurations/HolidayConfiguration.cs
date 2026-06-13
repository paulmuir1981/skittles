using Skittles.WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
{
    public void Configure(EntityTypeBuilder<Holiday> builder)
    {
        builder.HasKey(x => new { x.PlayerId, x.Date });
        builder.HasOne(x => x.Player)
            .WithMany(p => p.Holidays)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}