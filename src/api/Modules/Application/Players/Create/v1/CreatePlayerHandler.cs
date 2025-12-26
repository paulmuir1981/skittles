using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;

namespace Skittles.WebApi.Application.Players.Create.v1;

public sealed class CreatePlayerHandler(
    ILogger<CreatePlayerHandler> logger, 
    [FromKeyedServices("skittles:players")] IRepository<Player> repository)
        : IRequestHandler<CreatePlayerRequest, CreatePlayerResponse>
{
    public async Task<CreatePlayerResponse> Handle(CreatePlayerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new PlayerValidationException(nameof(request.Name), "Player name is required");
        }

        var nickname = request.Nickname ?? request.Name;

        // Check if nickname already exists
        var existingPlayer = await repository.FirstOrDefaultAsync(
            new NicknameSpec(nickname),
            cancellationToken);

        if (existingPlayer is not null)
        {
            throw new DuplicateNicknameException(nickname);
        }

        var player = Player.Create(request.Name, nickname, request.CanDrive, request.IsDeleted);
        await repository.AddAsync(player, cancellationToken);
        logger.LogInformation("player created {PlayerId}", player.Id);

        return new CreatePlayerResponse(player.Id);
    }
}
