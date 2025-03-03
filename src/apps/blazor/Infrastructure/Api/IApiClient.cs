namespace Skittles.Blazor.Infrastructure.Api;

public interface IApiClient
{
    Task<ICollection<Player>> GetPlayers(CancellationToken cancellationToken = default);
}