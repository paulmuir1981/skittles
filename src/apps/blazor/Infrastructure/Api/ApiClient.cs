using System.Net.Http.Json;

namespace Skittles.Blazor.Infrastructure.Api;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    public async Task<Player> GetPlayer(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await httpClient.GetAsync($"v1/players/{id}", cancellationToken);
        message.EnsureSuccessStatusCode();

        var player = await message.Content.ReadFromJsonAsync<Player>(cancellationToken);
        return player is null ? throw new ApiException("Response was null which was not expected.") : player!;
    }

    public async Task<IReadOnlyCollection<Player>> ListPlayers(CancellationToken cancellationToken = default)
    {
        var message = await httpClient.GetAsync("v1/players", cancellationToken);
        message.EnsureSuccessStatusCode();

        return await ReadFromJsonAsync<IReadOnlyCollection<Player>>(message, cancellationToken);
    }

    public async Task<CreatePlayerResponse> CreatePlayer(CreatePlayer player, CancellationToken cancellationToken = default)
    {
        var message = await httpClient.PostAsJsonAsync("v1/players", player, cancellationToken);
        message.EnsureSuccessStatusCode();

        return await ReadFromJsonAsync<CreatePlayerResponse>(message, cancellationToken);
    }

    public async Task<UpdatePlayerResponse> UpdatePlayer(Guid id, UpdatePlayer player, CancellationToken cancellationToken = default)
    {
        var message = await httpClient.PutAsJsonAsync($"v1/players/{id}", player, cancellationToken);
        message.EnsureSuccessStatusCode();

        return await ReadFromJsonAsync<UpdatePlayerResponse>(message, cancellationToken);
    }

    public async Task DeletePlayer(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await httpClient.DeleteAsync($"v1/players/{id}", cancellationToken);
        message.EnsureSuccessStatusCode();
    }

    private static async Task<T> ReadFromJsonAsync<T>(HttpResponseMessage message, CancellationToken cancellationToken)
    {
        var result = await message.Content.ReadFromJsonAsync<T>(cancellationToken);
        return result is null ? throw new ApiException("Response was null which was not expected.") : result!;
    }
}

[Serializable]
internal class ApiException : Exception
{
    public ApiException()
    {
    }

    public ApiException(string? message) : base(message)
    {
    }

    public ApiException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

public record Player(Guid Id, string Name);
public record CreatePlayer(string Name);
public record CreatePlayerResponse(Guid Id, string Name);
public record UpdatePlayer(string Name);
public record UpdatePlayerResponse(Guid Id, string Name);