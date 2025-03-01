namespace Skittles.Web;

public class SkittlesApiClient(HttpClient httpClient)
{
    public async Task<Player[]> GetPlayersAsync(CancellationToken cancellationToken = default)
    {
        List<Player>? players = null;
        await foreach (var player in httpClient.GetFromJsonAsAsyncEnumerable<Player>("/players", cancellationToken))
        {
            if (player is not null)
            {
                players ??= [];
                players.Add(player);
            }
        }
        return players?.ToArray() ?? [];
    }
}

public record Player(Guid Id, string Name);
