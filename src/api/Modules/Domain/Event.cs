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

public class Event : AuditableEntity, IAggregateRoot
{
    public Guid SeasonId { get; private set; }
    public DateOnly Date { get; private set; }
    public EventType EventType { get; private set; }
    public bool IsAway { get; private set; }
    public string? Opponent { get; private set; }
    public string Description { get; private set; } = default!;

    public Season Season { get; private set; } = null!;
    public IReadOnlyList<Leg> Legs { get; private set; } = [];
    public IReadOnlyList<Driver> Drivers { get; private set; } = [];
    public IReadOnlyList<Holiday> Holidays { get; private set; } = [];

    public static Event Create(
        Guid seasonId, DateOnly date, EventType eventType, bool isAway, string? opponent, string description)
    {
        var @event = new Event
        {
            SeasonId = seasonId,
            Date = date,
            EventType = eventType,
            IsAway = isAway,
            Opponent = opponent,
            Description = description
        };

        //@event.QueueDomainEvent(new EventCreated() { Event = @event });

        return @event;
    }
}