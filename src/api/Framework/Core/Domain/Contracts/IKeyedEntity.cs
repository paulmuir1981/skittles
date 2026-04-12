namespace Skittles.Framework.Core.Domain.Contracts;

public interface IKeyedEntity<out TId> : IEntity
{
    TId Id { get; }
}

public interface IKeyedEntity : IKeyedEntity<Guid>;