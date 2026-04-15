using NUnit.Framework;
using Skittles.WebApi.Domain;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Events.CreateEvent;

public abstract class GivenCreateEventRequest : ServerTestBase
{
    protected readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected HttpResponseMessage? _response;
    protected JsonNode? _node;

    protected async Task PostEvent(
        Guid seasonId, 
        DateOnly date, 
        EventType eventType, 
        string description, 
        bool isDeleted = false, 
        Guid? venueId = null, 
        Guid? opponentId = null)
    {
        var payload = new { seasonId, date, eventType, description, isDeleted, venueId, opponentId };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _response = await HttpClient!.PostAsync("/api/v1/events", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }
}