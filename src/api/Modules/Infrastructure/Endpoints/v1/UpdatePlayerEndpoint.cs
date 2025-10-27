using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Players.Update.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class UpdatePlayerEndpoint
{
    internal static RouteHandlerBuilder MapUpdatePlayerEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPut("/{id:guid}", async (Guid id, UpdatePlayerRequest request, ISender sender, CancellationToken ct) =>
            {
                request.Id = id;
                var response = await sender.Send(request, ct);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdatePlayerEndpoint))
            .WithSummary("update a player")
            .WithDescription("update a player")
            .Produces<UpdatePlayerResponse>()
            .MapToApiVersion(1);
    }
}