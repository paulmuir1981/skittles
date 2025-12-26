using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;

namespace Skittles.WebApi.Application.Players.Create.v1;

public sealed class CreatePlayerHandler(
    ILogger<CreatePlayerHandler> logger, [FromKeyedServices("skittles:players")] IRepository<Player> repository)
        : IRequestHandler<CreatePlayerRequest, CreatePlayerResponse>
{
    public async Task<CreatePlayerResponse> Handle(CreatePlayerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new PlayerValidationException(nameof(request.Name), "Player name is required");
        }

        var player = Player.Create(request.Name, request.Nickname ?? request.Name, request.CanDrive, request.IsDeleted);
        await repository.AddAsync(player, cancellationToken);
        logger.LogInformation("player created {PlayerId}", player.Id);

        return new CreatePlayerResponse(player.Id);
    }
}
