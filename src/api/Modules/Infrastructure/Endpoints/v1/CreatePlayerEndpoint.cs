using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Players.Create.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class CreatePlayerEndpoint
{
    internal static RouteHandlerBuilder MapCreatePlayerEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost("/", async (CreatePlayerRequest request, ISender sender, CancellationToken ct) =>
            {
                var response = await sender.Send(request, ct);
                return Results.Ok(response);
            })
            .WithName(nameof(CreatePlayerEndpoint))
            .WithSummary("creates a player")
            .WithDescription("creates a player")
            .Produces<CreatePlayerResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1);
    }
}