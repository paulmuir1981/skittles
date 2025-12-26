using NUnit.Framework;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Players.CreatePlayer;

public class GivenInvalidRequest : GivenCreatePlayerRequest
{
    [OneTimeSetUp]
    public async Task WhenPlayerCreatedWithInvalidData()
    {
        // Missing required name field
        var payload = new { nickname = "Johnny", canDrive = true, isDeleted = false };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _response = await HttpClient!.PostAsync("/api/v1/players", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }

    [Test]
    public void ThenResponseStatusBadRequest()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public void ThenResponseContainsErrorDetails()
    {
        Assert.That(_node, Is.Not.Null);
        Assert.That(
            _node!["detail"]!.GetValue<string>(), 
            Does.Contain("Validation failed for Name: Player name is required"));
    }
}