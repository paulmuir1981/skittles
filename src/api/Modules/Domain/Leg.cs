using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Leg : AuditableEntity, IAggregateRoot
{
    public Guid EventId { get; private set; }
    public byte Number { get; private set; }

    public Event Event { get; private set; } = null!;
    public IReadOnlyList<Score> Scores { get; private set; } = [];

    public static Leg Create(Guid eventId, byte number)
    {
        var leg = new Leg
        {
            EventId = eventId,
            Number = number
        };

        //leg.QueueDomainEvent(new LegCreated() { Leg = leg });

        return leg;
    }
}
