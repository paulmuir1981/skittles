using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Skittles.WebApi.Application.Pubs.Create.v1;

namespace Skittles.WebApi.Infrastructure.Endpoints.v1;

public static class CreatePubEndpoint
{
    internal static RouteHandlerBuilder MapCreatePubEndpoint(this IEndpointRouteBuilder endpoints)
    {
        return endpoints
            .MapPost(
                "/",
                async (CreatePubRequest request, ISender sender, CancellationToken ct) =>
                {
                    var response = await sender.Send(request, ct);
                    return Results.Ok(response);
                })
            .WithName(nameof(CreatePubEndpoint))
            .WithSummary("creates a pub")
            .WithDescription("creates a pub")
            .Produces<CreatePubResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .MapToApiVersion(1);
    }
}
