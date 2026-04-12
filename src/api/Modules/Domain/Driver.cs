using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public class Driver : AuditableEntity<(long PlayerId, Guid EventId)>, IAggregateRoot
{
    public long PlayerId { get; private set; }
    public Guid EventId { get; private set; }

    public Player Player { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    public static Driver Create(long playerId, Guid eventId)
    {
        var driver = new Driver
        {
            Id = (playerId, eventId)
        };

        //driver.QueueDomainEvent(new DriverCreated() { Driver = driver });

        return driver;
    }
}