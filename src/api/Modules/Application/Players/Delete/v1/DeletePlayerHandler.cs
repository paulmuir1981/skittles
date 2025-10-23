using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;

namespace Skittles.WebApi.Application.Players.Delete.v1;

public sealed class DeletePlayerHandler(
    ILogger<DeletePlayerHandler> logger, [FromKeyedServices("skittles:players")] IRepository<Player> repository)
        : IRequestHandler<DeletePlayerRequest>
{
    public async Task Handle(DeletePlayerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var player = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new PlayerNotFoundException(request.Id);

        await repository.DeleteAsync(player, cancellationToken);
        logger.LogInformation("player with id : {PlayerId} deleted", player.Id);
    }
}
