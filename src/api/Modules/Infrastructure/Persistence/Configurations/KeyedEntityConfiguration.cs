using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skittles.Framework.Core.Domain.Contracts;
using System.Reflection.Emit;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal abstract class KeyedEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IKeyedEntity<TId>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}

internal abstract class KeyedEntityConfiguration<TEntity> : KeyedEntityConfiguration<TEntity, Guid>
    where TEntity : class, IKeyedEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Id).HasDefaultValueSql("NEWID()");
    }
}

internal abstract class SoftDeletableKeyedEntityConfiguration<TEntity> : KeyedEntityConfiguration<TEntity>
    where TEntity : class, IKeyedEntity, ISoftDeletable
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);
    }
}