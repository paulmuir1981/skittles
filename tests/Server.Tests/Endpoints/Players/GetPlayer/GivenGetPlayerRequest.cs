using NUnit.Framework;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Players.GetPlayer;

public abstract class GivenGetPlayerRequest : ServerTestBase
{
    protected Guid _fakeId;
    protected HttpResponseMessage? _response;
    protected JsonNode? _node;

    [OneTimeSetUp]
    public async Task WhenRequested()
    {
        _response = await HttpClient!.GetAsync($"/api/v1/players/{_fakeId}");
        var json = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(json)!;
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }
}
