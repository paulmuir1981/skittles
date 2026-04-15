using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;
using Skittles.WebApi.Domain.Specifications;

namespace Skittles.WebApi.Application.Events.Create.v1;

public sealed class CreateEventHandler(
    ILogger<CreateEventHandler> logger,
    [FromKeyedServices("skittles:events")] IRepository<Event> repository)
        : IRequestHandler<CreateEventRequest, CreateEventResponse>
{
    public async Task<CreateEventResponse> Handle(CreateEventRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Description))
        {
            throw new PlayerValidationException(nameof(request.Description), "Event description is required");
        }

        // Check if description already exists for season
        var spec = new SeasonDescriptionSpec(request.SeasonId, request.Description);
        if (await repository.AnyAsync(spec, cancellationToken)) { 
            throw new DuplicateSeasonDescriptionException(request.SeasonId, request.Description);
        }

        var @event = Event.Create(
            request.SeasonId, 
            request.Date, 
            request.EventType, 
            request.Description, 
            request.IsDeleted, 
            request.VenueId, 
            request.OpponentId);

        await repository.AddAsync(@event, cancellationToken);
        logger.LogInformation("event created {EventId}", @event.Id);

        return new CreateEventResponse(@event.Id);
    }
}