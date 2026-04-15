using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Assertions;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Events.ListEvents;

[TestFixture(null, 1)]
[TestFixture(false, 1)]
[TestFixture(true, 2)]
public class GivenValidRequest : ServerTestBase
{
    private Guid _seasonId;
    private Guid _eventId1;
    private Guid _eventId2;

    private readonly bool? _includeDeleted;
    private readonly int _expectedCount;
    private HttpResponseMessage? _response;
    private JsonNode? _node;

    private static readonly (
        DateOnly Date, 
        EventType EventType, 
        string Description,
        bool IsDeleted, 
        Guid? VenueId,
        string? VenueName,
        Guid? OpponentId,
        string? OpponentName)
        Event1Data = (
        DateOnly.FromDateTime(DateTime.UtcNow), 
        EventType.Practice, 
        "I am a description", 
        false,
        null,
        null,
        null,
        null);

    private static readonly (
        DateOnly Date,
        EventType EventType,
        string Description,
        bool IsDeleted,
        Guid? VenueId,
        string? VenueName,
        Guid? OpponentId,
        string? OpponentName) 
        Event2Data = (
        DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)),
        EventType.Meeting,
        "I am another description",
        true,
        null,
        null,
        null,
        null);

    public GivenValidRequest(bool? includeDeleted, int expectedCount)
    {
        _includeDeleted = includeDeleted;
        _expectedCount = expectedCount;
    }

    [OneTimeSetUp]
    public async Task WhenRequested()
    {
        var includeDeletedPath = _includeDeleted.HasValue ? $"&includeDeleted={_includeDeleted.Value}" : "";
        var path = $"/api/v1/events?seasonId={_seasonId}{includeDeletedPath}";
        _response = await HttpClient!.GetAsync(path);
        var json = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(json)!;
    }

    protected override void SetupSkittlesDbContext()
    {
        using var scope = Factory!.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var season = new SeasonBuilder().Build();

        var event1 = new EventBuilder()
            .WithSeasonId(Guid.Empty)
            .WithDate(Event1Data.Date)
            .WithEventType(Event1Data.EventType)
            .WithDescription(Event1Data.Description)
            .WithIsDeleted(Event1Data.IsDeleted)
            .Build();
        season.Events.Add(event1);

        var event2 = new EventBuilder()
            .WithSeasonId(Guid.Empty)
            .WithDate(Event2Data.Date)
            .WithEventType(Event2Data.EventType)
            .WithDescription(Event2Data.Description)
            .WithIsDeleted(Event2Data.IsDeleted)
            .Build();
        season.Events.Add(event2);

        ctx.Seasons.Add(season);
        ctx.SaveChanges();

        _seasonId = season.Id;
        _eventId1 = event1.Id;
        _eventId2 = event2.Id;
    }

    [Test]
    public void ThenResponseStatusOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }

    [Test]
    public void ThenResultExpected()
    {
        Assert.That(_node, Has.Exactly(_expectedCount).Items);

        var sorted = ((JsonArray)_node!).OrderBy(x => (bool?)x!["isDeleted"]).ToList();

        EventAssertions.AssertEventResponse(sorted[0]!, _eventId1, Event1Data);

        if (_includeDeleted == true)
        {
            EventAssertions.AssertEventResponse(sorted[1]!, _eventId2, Event2Data);
        }
    }
}
