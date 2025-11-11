using Ardalis.Specification;
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
        var spec = new ListPlayerSpec(request.IncludeDeleted);
        var items = await repository.ListAsync(spec, cancellationToken).ConfigureAwait(false);
        return items;
    }

    public class ListPlayerSpec : Specification<Player, PlayerResponse>
    {
        public ListPlayerSpec(bool includeDeleted)
        {
            if (!includeDeleted)
            {
                Query.Where(x => !x.IsDeleted);
            }
            Query.Select(x => new PlayerResponse(x.Id, x.Name, x.Nickname, x.CanDrive, x.IsDeleted));
        }
    }
}
