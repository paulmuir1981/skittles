using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;

namespace Skittles.WebApi.Application.Players.Update.v1;

public sealed class UpdatePlayerHandler(
    ILogger<UpdatePlayerHandler> logger, 
    [FromKeyedServices("skittles:players")] IRepository<Player> repository)
        : IRequestHandler<UpdatePlayerRequest, UpdatePlayerResponse>
{
    public async Task<UpdatePlayerResponse> Handle(UpdatePlayerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var player = await repository.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw new PlayerNotFoundException(request.Id);

        var newNickname = request.Nickname ?? player.Nickname;

        // Only check for duplicate if nickname is actually changing
        if (!newNickname.Equals(player.Nickname, StringComparison.OrdinalIgnoreCase))
        {
            var existingPlayer = await repository.FirstOrDefaultAsync(
                new NicknameSpec(newNickname),
                cancellationToken);

            if (existingPlayer is not null)
            {
                throw new DuplicateNicknameException(newNickname);
            }
        }

        var updatedPlayer = player.Update(request.Name, request.Nickname, request.CanDrive, request.IsDeleted);
        await repository.UpdateAsync(updatedPlayer, cancellationToken);
        logger.LogInformation("player with id : {PlayerId} updated.", player.Id);

        return new UpdatePlayerResponse(player.Id);
    }
}
