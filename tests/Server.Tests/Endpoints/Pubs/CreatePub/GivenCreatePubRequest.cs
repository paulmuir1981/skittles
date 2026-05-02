using NUnit.Framework;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Pubs.CreatePub;

public abstract class GivenCreatePubRequest : ServerTestBase
{
    protected readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected HttpResponseMessage? _response;
    protected JsonNode? _node;

    protected async Task PostPub(
        string name,
        string postcode,
        bool isDeleted = false)
    {
        var payload = new { name, postcode, isDeleted };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _response = await HttpClient!.PostAsync("/api/v1/pubs", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }
}
