using NUnit.Framework;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Assertions;

public static class EventAssertions
{
    public static void AssertEventResponse(
        JsonNode @event,
        Guid expectedId,
        (
            DateOnly Date,
            EventType EventType,
            string Description,
            bool IsDeleted,
            Guid? VenueId,
            string? VenueName,
            Guid? OpponentId,
            string? OpponentName) expectedData)
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(@event, Has.Exactly(9).Items);
            Assert.That(@event["id"]!.GetValue<Guid>(), Is.EqualTo(expectedId));

            var dateNode = @event["date"];
            Assert.That(dateNode, Is.Not.Null);
            var date = DateOnly.Parse(dateNode!.GetValue<string>());
            Assert.That(date, Is.EqualTo(expectedData.Date));

            var eventTypeNode = @event["eventType"];
            Assert.That(eventTypeNode, Is.Not.Null);
            var eventType = (EventType)eventTypeNode!.GetValue<byte>();
            Assert.That(eventType, Is.EqualTo(expectedData.EventType));

            Assert.That(@event["description"]!.GetValue<string>(), Is.EqualTo(expectedData.Description));
            Assert.That(@event["isDeleted"]!.GetValue<bool>(), Is.EqualTo(expectedData.IsDeleted));

            var venueIdNode = @event["venueId"];
            Assert.That(venueIdNode?.GetValue<Guid?>(), Is.EqualTo(expectedData.VenueId));

            var venueNameNode = @event["venueName"];
            Assert.That(venueNameNode?.GetValue<string?>(), Is.EqualTo(expectedData.VenueName));

            var opponentIdNode = @event["opponentId"];
            Assert.That(opponentIdNode?.GetValue<Guid?>(), Is.EqualTo(expectedData.OpponentId));

            var opponentNameNode = @event["opponentName"];
            Assert.That(opponentNameNode?.GetValue<string?>(), Is.EqualTo(expectedData.OpponentName));
        }
    }

    public static void AssertEventInDb(
        SkittlesDbContext context,
        Guid expectedId,
        (
            Guid SeasonId,
            DateOnly Date,
            EventType EventType,
            string Description,
            bool IsDeleted,
            Guid? VenueId,
            Guid? OpponentId) expectedData)
    {
        var @event = context.Events.FirstOrDefault(e => e.Id == expectedId);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(@event, Is.Not.Null);
            Assert.That(@event!.Id, Is.EqualTo(expectedId));
            Assert.That(@event.SeasonId, Is.EqualTo(expectedData.SeasonId));
            Assert.That(@event.Date, Is.EqualTo(expectedData.Date));
            Assert.That(@event.EventType, Is.EqualTo(expectedData.EventType));
            Assert.That(@event.Description, Is.EqualTo(expectedData.Description));
            Assert.That(@event.IsDeleted, Is.EqualTo(expectedData.IsDeleted));
            Assert.That(@event.VenueId, Is.EqualTo(expectedData.VenueId));
            Assert.That(@event.OpponentId, Is.EqualTo(expectedData.OpponentId));
        }
    }

    public static void AssertPlayerDeletedFromDb(SkittlesDbContext context, Guid playerId)
    {
        var player = context.Players.FirstOrDefault(p => p.Id == playerId);
        Assert.That(player, Is.Null, $"Player {playerId} should be deleted from database");
    }
}