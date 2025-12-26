using NUnit.Framework;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Players.CreatePlayer;

public abstract class GivenCreatePlayerRequest : ServerTestBase
{
    protected readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected HttpResponseMessage? _response;
    protected JsonNode? _node;

    protected async Task PostPlayer(string name, string nickname, bool canDrive, bool isDeleted)
    {
        var payload = new { name, nickname, canDrive, isDeleted };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _response = await HttpClient!.PostAsync("/api/v1/players", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }
}