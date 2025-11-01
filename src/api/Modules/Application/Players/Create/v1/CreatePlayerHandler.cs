using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Application.Players.Create.v1;

public sealed class CreatePlayerHandler(
    ILogger<CreatePlayerHandler> logger, [FromKeyedServices("skittles:players")] IRepository<Player> repository)
        : IRequestHandler<CreatePlayerRequest, CreatePlayerResponse>
{
    public async Task<CreatePlayerResponse> Handle(CreatePlayerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var player = Player.Create(request.Name!, request.Nickname!, request.CanDrive);
        await repository.AddAsync(player, cancellationToken);
        logger.LogInformation("player created {PlayerId}", player.Id);

        return new CreatePlayerResponse(player.Id);
    }
}
