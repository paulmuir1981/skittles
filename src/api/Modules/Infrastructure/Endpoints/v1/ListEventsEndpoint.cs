using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Events.List.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class ListEventsEndpoint
{
    internal static RouteHandlerBuilder MapListEventsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/", async (ISender sender, Guid seasonId, CancellationToken ct, bool includeDeleted = false) =>
            {
                var response = await sender.Send(new ListEventsRequest(seasonId, includeDeleted), ct);
                return Results.Ok(response);
            })
            .WithName(nameof(ListEventsEndpoint))
            .WithSummary("gets list of events")
            .WithDescription("gets list of events")
            .Produces<List<EventResponse>>()
            .MapToApiVersion(1);
    }
}