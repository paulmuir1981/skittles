using NUnit.Framework;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Pubs.CreatePub;

public class GivenInvalidRequest : GivenCreatePubRequest
{
    [Test]
    public async Task ThenResponseStatusCodeBadRequestWhenNameEmpty()
    {
        await PostPub(string.Empty, "SW1A 1AA");
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task ThenResponseStatusCodeBadRequestWhenPostcodeEmpty()
    {
        await PostPub("The Red Lion", string.Empty);
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task ThenResponseStatusCodeBadRequestWhenNameNull()
    {
        var payload = new { name = (string?)null, postcode = "SW1A 1AA", isDeleted = false };
        var json = System.Text.Json.JsonSerializer.Serialize(payload);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        _response = await HttpClient!.PostAsync("/api/v1/pubs", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = System.Text.Json.Nodes.JsonNode.Parse(responseContent)!;

        Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
