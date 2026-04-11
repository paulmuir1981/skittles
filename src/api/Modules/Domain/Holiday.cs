using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Holiday : AuditableEntity<(Guid PlayerId, Guid EventId)>, IAggregateRoot
{
    public Guid PlayerId { get; private set; }
    public Guid EventId { get; private set; }
    public bool IsProvisional { get; private set; }

    public Player Player { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    public static Holiday Create(Guid playerId, Guid eventId)
    {
        var holiday = new Holiday
        {
            Id = (playerId, eventId)
        };

        //holiday.QueueDomainEvent(new HolidayCreated() { Holiday = holiday });

        return holiday;
    }
}