using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Application.Events.List.v1;

public sealed record EventResponse(
    Guid Id,
    Guid? OpponentId,
    string? OpponentName,
    Guid? VenueId, 
    string? VenueName,
    DateOnly Date, 
    EventType EventType, 
    string Description, 
    bool IsDeleted);