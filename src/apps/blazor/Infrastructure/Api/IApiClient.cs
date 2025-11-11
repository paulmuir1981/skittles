namespace Skittles.Blazor.Infrastructure.Api;

public interface IApiClient
{
    Task<Player> GetPlayer(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Player>> ListPlayers(bool includeDeleted, CancellationToken cancellationToken = default);
    Task<CreatePlayerResponse> CreatePlayer(CreatePlayer player, CancellationToken cancellationToken = default);
    Task<UpdatePlayerResponse> UpdatePlayer(Guid id, UpdatePlayer player, CancellationToken cancellationToken = default);
    Task DeletePlayer(Guid id, CancellationToken cancellationToken = default);
}