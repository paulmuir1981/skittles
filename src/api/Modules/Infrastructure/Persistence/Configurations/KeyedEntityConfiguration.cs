using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skittles.Framework.Core.Domain.Contracts;

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
    where TEntity : class, IKeyedEntity;