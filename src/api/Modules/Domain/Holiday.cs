using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Holiday : BaseEntity, IAggregateRoot
{
    public Guid PlayerId { get; private set; }
    public Guid EventId { get; private set; }

    public Player Player { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    public static Holiday Create(Guid playerId, Guid eventId)
    {
        var holiday = new Holiday
        {
            PlayerId = playerId,
            EventId = eventId
        };

        //holiday.QueueDomainEvent(new HolidayCreated() { Holiday = holiday });

        return holiday;
    }
}