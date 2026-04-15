using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Events.CreateEvent;

public class GivenDuplicateNickname : GivenCreateEventRequest
{
    private const string SharedDescription = "UniqueDescription";
    private Guid _seasonId;

    [OneTimeSetUp]
    public void SetupExistingEvent()
    {
        // Create a event with a specific nickname
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var season = new SeasonBuilder().Build();
        var @event = new EventBuilder().WithDescription(SharedDescription).Build();
        season.Events.Add(@event);

        context.Seasons.Add(season);
        context.SaveChanges();

        _seasonId = season.Id;
    }

    [OneTimeSetUp]
    public async Task WhenEventCreatedWithDuplicateDescription()
    {
        var payload = new { seasonId = _seasonId, description = SharedDescription };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _response = await HttpClient!.PostAsync("/api/v1/events", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }

    [Test]
    public void ThenResponseStatusConflict()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
    }

    [Test]
    public void ThenResponseContainsDuplicateDescriptionError()
    {
        Assert.That(_node, Is.Not.Null);
        Assert.That(
            _node!["detail"]!.GetValue<string>(), 
            Does.Contain($"description '{SharedDescription}' already exists"));
    }
}