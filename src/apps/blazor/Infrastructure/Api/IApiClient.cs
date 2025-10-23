namespace Skittles.Blazor.Infrastructure.Api;

public interface IApiClient
{
    Task<IReadOnlyCollection<Player>> GetPlayers(CancellationToken cancellationToken = default);
}