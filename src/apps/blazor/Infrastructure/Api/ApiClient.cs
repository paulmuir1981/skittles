using System.Net.Http.Json;

namespace Skittles.Blazor.Infrastructure.Api;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    public async Task<ICollection<Player>> GetPlayers(CancellationToken cancellationToken = default)
    {
        List<Player>? players = null;
        //todo i don't think this is needed. example app had a maximum number and would quit after that. just deserialise the array/list/whatever
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