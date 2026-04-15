using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Pub : AuditableEntity, IAggregateRoot, IKeyedEntity, ISoftDeletable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Postcode { get; private set; } = default!;
    public bool IsDeleted { get; private set; }
    public ICollection<Event> HostedEvents { get; private set; } = [];
    public ICollection<Event> OppositionEvents { get; private set; } = [];

    public static Pub Create(string name, string postcode, bool isDeleted = false)
    {
        var pub = new Pub
        {
            Name = name,
            Postcode = postcode,
            IsDeleted = isDeleted
        };

        //pub.QueueDomainEvent(new PubCreated() { Pub = pub });

        return pub;
    }
}
