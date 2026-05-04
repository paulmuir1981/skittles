using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal sealed class PlayerConfiguration : SoftDeletableKeyedEntityConfiguration<Player>
{
    public override void Configure(EntityTypeBuilder<Player> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).HasMaxLength(100);
        builder.Property(x => x.Nickname).HasMaxLength(100);
        builder.HasIndex(x => x.Nickname).IsUnique();
    }
}