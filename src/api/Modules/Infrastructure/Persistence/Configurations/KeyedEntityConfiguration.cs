using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Infrastructure.Persistence.Configurations;

internal static class AuditableEntityConfigurationExtensions
{
    internal static void ConfigureAuditable<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IAuditable
    {
        builder.Property(b => b.Created).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(b => b.CreatedBy).HasDefaultValue(Guid.Empty);
    }
}

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

internal abstract class AuditableEntityConfiguration<TEntity> : KeyedEntityConfiguration<TEntity>
    where TEntity : class, IKeyedEntity, IAuditable
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.ConfigureAuditable();
    }
}

internal abstract class SoftDeletableKeyedEntityConfiguration<TEntity> : AuditableEntityConfiguration<TEntity>
    where TEntity : class, IKeyedEntity, ISoftDeletable, IAuditable
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.IsDeleted).HasDefaultValue(false);
    }
}

internal abstract class NonKeyedAuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IAuditable
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ConfigureAuditable();
    }
}