using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Events.Create.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class CreateEventEndpoint
{
    internal static RouteHandlerBuilder MapCreateEventEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/",
                async (CreateEventRequest request, ISender sender, CancellationToken ct) =>
                {
                    var response = await sender.Send(request, ct);
                    return Results.Ok(response);
                })
            .WithName(nameof(CreateEventEndpoint))
            .WithSummary("creates an event")
            .WithDescription("creates an event")
            .Produces<CreateEventResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1);
    }
}