using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Players.List.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class ListPlayersEndpoint
{
    internal static RouteHandlerBuilder MapListPlayersEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/", async (ISender sender, bool includeDeleted, CancellationToken ct) =>
            {
                var response = await sender.Send(new ListPlayersRequest(includeDeleted), ct);
                return Results.Ok(response);
            })
            .WithName(nameof(ListPlayersEndpoint))
            .WithSummary("gets list of players")
            .WithDescription("gets list of players")
            .Produces<List<PlayerResponse>>()
            .MapToApiVersion(1);
    }
}