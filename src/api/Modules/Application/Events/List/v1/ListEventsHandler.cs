using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Application.Events.List.v1;

public sealed class ListEventsHandler(
    [FromKeyedServices("skittles:events")] IReadRepository<Event> repository)
    : IRequestHandler<ListEventsRequest, List<EventResponse>>
{
    public async Task<List<EventResponse>> Handle(ListEventsRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var spec = new ListEventsSpec(request.SeasonId, request.IncludeDeleted);
        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        return items;
    }

    public class ListEventsSpec : Specification<Event, EventResponse>
    {
        public ListEventsSpec(Guid seasonId, bool includeDeleted)
        {
            Query.Where(x => x.SeasonId == seasonId);
            if (!includeDeleted)
            {
                Query.Where(x => !x.IsDeleted);
            }
            Query.Select(
                x => new EventResponse(
                    x.Id,
                    x.OpponentId,
                    x.Opponent == null ? null : x.Opponent.Name,
                    x.VenueId,
                    x.Venue == null ? null : x.Venue.Name,
                    x.Date,
                    x.EventType,
                    x.Description,
                    x.IsDeleted));
        }
    }
}
