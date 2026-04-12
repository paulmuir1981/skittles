using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class PubConfiguration : KeyedEntityConfiguration<Pub>
{
    public override void Configure(EntityTypeBuilder<Pub> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Postcode).HasMaxLength(10);
    }
}