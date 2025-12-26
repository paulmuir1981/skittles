using NUnit.Framework;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Players.UpdatePlayer;

public abstract class GivenUpdatePlayerRequest : ServerTestBase
{
    protected readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected Guid _playerId;
    protected HttpResponseMessage? _response;
    protected JsonNode? _node;

    protected async Task PutPlayer(Guid id, string? name, string? nickname, bool canDrive, bool isDeleted)
    {
        var payload = new { name, nickname, canDrive, isDeleted };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _response = await HttpClient!.PutAsync($"/api/v1/players/{id}", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }
}