using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Application.Players.List.v1;

public sealed class ListPlayersHandler(
    [FromKeyedServices("skittles:players")] IReadRepository<Player> repository)
    : IRequestHandler<ListPlayersRequest, List<PlayerResponse>>
{
    public async Task<List<PlayerResponse>> Handle(ListPlayersRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var items = await repository.ListAsync(cancellationToken).ConfigureAwait(false);
        return [.. items.Select(player => new PlayerResponse(player.Id, player.Name, player.Nickname, player.CanDrive))];
    }
}
