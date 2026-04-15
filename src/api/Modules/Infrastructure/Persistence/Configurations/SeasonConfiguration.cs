using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class SeasonConfiguration : KeyedEntityConfiguration<Season>
{
    public override void Configure(EntityTypeBuilder<Season> builder)
    {
        base.Configure(builder);
        builder.HasIndex(x => new { x.Year }).IsUnique();
    }
}