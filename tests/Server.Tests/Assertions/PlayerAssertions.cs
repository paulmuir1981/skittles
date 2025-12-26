using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Assertions;

public static class PlayerAssertions
{
    public static void AssertPlayerResponse(
        JsonNode player,
        Guid expectedId,
        (string Name, string Nickname, bool CanDrive, bool IsDeleted) expectedData)
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(player, Has.Exactly(5).Items);
            Assert.That(player["id"]!.GetValue<Guid>(), Is.EqualTo(expectedId));
            Assert.That(player["name"]!.GetValue<string>(), Is.EqualTo(expectedData.Name));
            Assert.That(player["nickname"]!.GetValue<string>(), Is.EqualTo(expectedData.Nickname));
            Assert.That(player["canDrive"]!.GetValue<bool>(), Is.EqualTo(expectedData.CanDrive));
            Assert.That(player["isDeleted"]!.GetValue<bool>(), Is.EqualTo(expectedData.IsDeleted));
        }
    }

    public static void AssertPlayerInDb(
        SkittlesDbContext context,
        Guid expectedId,
        (string Name, string Nickname, bool CanDrive, bool IsDeleted) expectedData)
    {
        var player = context.Players.FirstOrDefault(p => p.Id == expectedId);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(player, Is.Not.Null);
            Assert.That(player!.Id, Is.EqualTo(expectedId));
            Assert.That(player.Name, Is.EqualTo(expectedData.Name));
            Assert.That(player.Nickname, Is.EqualTo(expectedData.Nickname));
            Assert.That(player.CanDrive, Is.EqualTo(expectedData.CanDrive));
            Assert.That(player.IsDeleted, Is.EqualTo(expectedData.IsDeleted));
        }
    }

    public static void AssertPlayerDeletedFromDb(SkittlesDbContext context, Guid playerId)
    {
        var player = context.Players.FirstOrDefault(p => p.Id == playerId);
        Assert.That(player, Is.Null, $"Player {playerId} should be deleted from database");
    }
}