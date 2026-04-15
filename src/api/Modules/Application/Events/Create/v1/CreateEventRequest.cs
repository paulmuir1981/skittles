using MediatR;
using Skittles.WebApi.Domain;
using System.ComponentModel.DataAnnotations;

namespace Skittles.WebApi.Application.Events.Create.v1;

public sealed record CreateEventRequest(
    Guid SeasonId,
    DateOnly Date,
    EventType EventType,
    [Required(ErrorMessage = "Event description is required")]
    string Description,
    bool IsDeleted = false,
    Guid? VenueId = null,
    Guid? OpponentId = null) : IRequest<CreateEventResponse>;