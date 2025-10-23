using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Players.Delete.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class DeletePlayerEndpoint
{
    internal static RouteHandlerBuilder MapPlayerDeleteEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapDelete("/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                await sender.Send(new DeletePlayerRequest(id), ct);
                return Results.NoContent();
            })
            .WithName(nameof(DeletePlayerEndpoint))
            .WithSummary("deletes player by id")
            .WithDescription("deletes player by id")
            .Produces(StatusCodes.Status204NoContent)
            .MapToApiVersion(1);
    }
}