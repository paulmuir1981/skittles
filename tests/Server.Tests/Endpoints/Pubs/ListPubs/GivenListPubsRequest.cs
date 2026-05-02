using NUnit.Framework;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Pubs.ListPubs;

public abstract class GivenListPubsRequest : ServerTestBase
{
    protected readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected HttpResponseMessage? _response;
    protected JsonNode? _node;

    protected async Task GetPubs(bool includeDeleted = false)
    {
        var url = $"/api/v1/pubs?includeDeleted={includeDeleted}";
        _response = await HttpClient!.GetAsync(url);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }
}
