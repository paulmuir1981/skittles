using System.Net.Http.Json;

namespace Skittles.Blazor.Infrastructure.Api;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    public async Task<IReadOnlyCollection<Player>> GetPlayers(CancellationToken cancellationToken = default)
    {
        var message = await httpClient.GetAsync("api/v1/players", cancellationToken);
        message.EnsureSuccessStatusCode();
        return (await message.Content.ReadFromJsonAsync<IReadOnlyCollection<Player>>(cancellationToken)) ?? [];
    }
}

public record Player(Guid Id, string Name);