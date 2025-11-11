using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Domain.Exceptions;

namespace Skittles.WebApi.Application.Players.Get.v1;

public sealed class GetPlayerHandler([FromKeyedServices("skittles:players")] IReadRepository<Player> repository)
    : IRequestHandler<GetPlayerRequest, GetPlayerResponse>
{
    public async Task<GetPlayerResponse> Handle(GetPlayerRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var player = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new PlayerNotFoundException(request.Id);

        return new GetPlayerResponse(player.Id, player.Name, player.Nickname, player.CanDrive, player.IsDeleted);
    }
}
