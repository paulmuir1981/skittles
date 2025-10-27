using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Players.Get.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class GetPlayerEndpoint
{
    internal static RouteHandlerBuilder MapGetPlayerEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var response = await sender.Send(new GetPlayerRequest(id), ct);
                return Results.Ok(response);
            })
            .WithName(nameof(GetPlayerEndpoint))
            .WithSummary("gets player by id")
            .WithDescription("gets player by id")
            .Produces<GetPlayerResponse>()
            .MapToApiVersion(1);
    }
}