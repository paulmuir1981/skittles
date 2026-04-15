using Skittles.WebApi.Domain;

namespace Skittles.Server.Tests.Builders;

public class EventBuilder
{
    private Guid? _seasonId;
    private DateOnly? _date;
    private EventType? _eventType;
    private string? _description;
    private bool? _isDeleted;
    private Guid? _venueId;
    private Guid? _opponentId;

    public Event Build()
    {
        return Event.Create(
            _seasonId ?? Guid.NewGuid(),
            _date ?? DateOnly.FromDateTime(DateTime.UtcNow),
            _eventType ?? EventType.Meeting,
            _description ?? "Test event description",
            _isDeleted ?? false,
            _venueId,
            _opponentId);
    }

    public EventBuilder WithSeasonId(Guid seasonId)
    {
        _seasonId = seasonId;
        return this;
    }

    public EventBuilder WithDate(DateOnly date)
    {
        _date = date;
        return this;
    }

    public EventBuilder WithEventType(EventType eventType)
    {
        _eventType = eventType;
        return this;
    }

    public EventBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public EventBuilder WithIsDeleted(bool isDeleted)
    {
        _isDeleted = isDeleted;
        return this;
    }

    public EventBuilder WithVenueId(Guid? venueId)
    {
        _venueId = venueId;
        return this;
    }

    public EventBuilder WithOpponentId(Guid? opponentId)
    {
        _opponentId = opponentId;
        return this;
    }
}
