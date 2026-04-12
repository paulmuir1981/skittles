using Skittles.Framework.Core.Domain;
using Skittles.Framework.Core.Domain.Contracts;

namespace Skittles.WebApi.Domain;

public enum EventType : byte
{
    Meeting = 1,
    Practice = 2,
    League = 3,
    Cup = 4
}

public class Event : AuditableEntity, IAggregateRoot, IKeyedEntity
{
    public Guid Id { get; private set; }
    public Guid SeasonId { get; private set; }
    public Guid? OpponentId { get; private set; }
    public Guid? VenueId { get; private set; }
    public DateOnly Date { get; private set; }
    public EventType EventType { get; private set; }
    public string Description { get; private set; } = default!;

    public Season Season { get; private set; } = null!;
    public Pub? Opponent { get; private set; }
    public Pub? Venue { get; private set; }
    public IReadOnlyList<Leg> Legs { get; private set; } = [];
    public IReadOnlyList<Driver> Drivers { get; private set; } = [];
    public IReadOnlyList<Holiday> Holidays { get; private set; } = [];

    public static Event Create(
        Guid seasonId, 
        DateOnly date, 
        EventType eventType, 
        string description, 
        Guid? venueId = null, 
        Guid? opponentId = null)
    {
        var @event = new Event
        {
            SeasonId = seasonId,
            Date = date,
            EventType = eventType,
            VenueId = venueId,
            OpponentId = opponentId,
            Description = description
        };

        //@event.QueueDomainEvent(new EventCreated() { Event = @event });

        return @event;
    }
}