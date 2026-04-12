using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Holiday : AuditableEntity<(long PlayerId, Guid EventId)>, IAggregateRoot
{
    public long PlayerId { get; private set; }
    public Guid EventId { get; private set; }
    public bool IsProvisional { get; private set; }

    public Player Player { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    public static Holiday Create(long playerId, Guid eventId)
    {
        var holiday = new Holiday
        {
            Id = (playerId, eventId)
        };

        //holiday.QueueDomainEvent(new HolidayCreated() { Holiday = holiday });

        return holiday;
    }
}