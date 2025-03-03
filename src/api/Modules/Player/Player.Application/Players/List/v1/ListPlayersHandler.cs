using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Player.Domain;

namespace Skittles.WebApi.Player.Application.Players.List.v1;

public sealed class ListPlayersHandler()
    //todo [FromKeyedServices("player")] IReadRepository<PlayerItem> repository)
    : IRequestHandler<ListPlayersRequest, List<PlayerResponse>>
{
    public async Task<List<PlayerResponse>> Handle(ListPlayersRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        //todo var list = await repository.ListAsync(cancellationToken).ConfigureAwait(false);
        var list = Enumerable.Range(1, 15).Select(
            index => new PlayerResponse(Guid.NewGuid(), $"Player {index}"));
        return [.. list];
        //todo return [.. list.Select(x => new PlayerResponse(x.Id, x.Name))];
    }
}
