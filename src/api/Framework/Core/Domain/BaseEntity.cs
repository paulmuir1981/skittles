using Skittles.Framework.Core.Domain.Contracts;
using Skittles.Framework.Core.Domain.Events;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skittles.Framework.Core.Domain;

public abstract class BaseEntity : IEntity
{
    [NotMapped]
    public Collection<DomainEvent> DomainEvents { get; } = [];

    public void QueueDomainEvent(DomainEvent @event)
    {
        if (!DomainEvents.Contains(@event))
            DomainEvents.Add(@event);
    }
}
