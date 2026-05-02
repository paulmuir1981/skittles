using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Pubs.List.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class ListPubsEndpoint
{
    internal static RouteHandlerBuilder MapListPubsEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/", async (ISender sender, CancellationToken ct, bool includeDeleted = false) =>
            {
                var response = await sender.Send(new ListPubsRequest(includeDeleted), ct);
                return Results.Ok(response);
            })
            .WithName(nameof(ListPubsEndpoint))
            .WithSummary("gets list of pubs")
            .WithDescription("gets list of pubs")
            .Produces<List<PubResponse>>()
            .MapToApiVersion(1);
    }
}
