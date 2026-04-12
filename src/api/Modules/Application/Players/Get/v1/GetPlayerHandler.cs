using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;
using Skittles.WebApi.Domain.Specifications;

namespace Skittles.WebApi.Application.Players.Get.v1;

public sealed class GetPlayerHandler([FromKeyedServices("skittles:players")] IReadRepository<Player> repository)
    : IRequestHandler<GetPlayerRequest, GetPlayerResponse>
{
    public async Task<GetPlayerResponse> Handle(GetPlayerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var player = await repository.FirstOrDefaultAsync(new IdSpec(request.Id), cancellationToken)
            ?? throw new PlayerNotFoundException(request.Id);

        return new GetPlayerResponse(player.PlayerId, player.Name, player.Nickname, player.CanDrive, player.IsDeleted);
    }
}
