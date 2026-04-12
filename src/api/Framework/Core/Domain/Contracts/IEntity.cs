using Skittles.Framework.Core.Domain.Events;
using System.Collections.ObjectModel;

namespace Skittles.Framework.Core.Domain.Contracts;

public interface IEntity
{
    Collection<DomainEvent> DomainEvents { get; }
}