using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Driver : BaseEntity, IAggregateRoot
{
    public Guid PlayerId { get; private set; }
    public Guid EventId { get; private set; }

    public Player Player { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    public static Driver Create(Guid playerId, Guid eventId)
    {
        var driver = new Driver
        {
            PlayerId = playerId,
            EventId = eventId
        };

        //driver.QueueDomainEvent(new DriverCreated() { Driver = driver });

        return driver;
    }
}