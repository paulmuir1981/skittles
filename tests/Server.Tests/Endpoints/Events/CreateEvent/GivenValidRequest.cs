using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Assertions;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text.Json;

namespace Skittles.Server.Tests.Endpoints.Events.CreateEvent;

public class GivenValidRequest : GivenCreateEventRequest
{
    private static readonly (
        Guid SeasonId, 
        DateOnly Date, 
        EventType EventType, 
        string Description, 
        bool IsDeleted, 
        Guid? VenueId, 
        Guid? OpponentId) 
        EventData = (
        Guid.NewGuid(), 
        DateOnly.FromDateTime(DateTime.Now), 
        EventType.Practice, 
        "I am a description", 
        true, 
        null, 
        null);

    private Guid _createdEventId;

    [OneTimeSetUp]
    public async Task WhenEventCreated()
    {
        await PostEvent(
            EventData.SeasonId, 
            EventData.Date, 
            EventData.EventType, 
            EventData.Description, 
            EventData.IsDeleted,
            EventData.VenueId, 
            EventData.OpponentId);
        
        if (_response!.StatusCode == HttpStatusCode.OK)
        {
            var createResponse = JsonSerializer.Deserialize<CreateEventResponse>(_node!.ToJsonString(), JsonSerializerOptions);
            _createdEventId = createResponse?.Id ?? Guid.Empty;
        }
    }

    [Test]
    public void ThenResponseStatusOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResponseContainsCreatedEventId()
    {
        Assert.That(_createdEventId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(_node!["id"]!.GetValue<Guid>(), Is.EqualTo(_createdEventId));
    }

    [Test]
    public void ThenEventExistsInDatabase()
    {
        // Query the database directly to verify the event was created
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();
        EventAssertions.AssertEventInDb(context, _createdEventId, EventData);
    }

    private record CreateEventResponse(Guid Id);
}
