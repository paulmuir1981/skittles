using MediatR;

namespace Skittles.WebApi.Application.Events.List.v1;

public record ListEventsRequest(Guid SeasonId, bool IncludeDeleted) : IRequest<List<EventResponse>>;
