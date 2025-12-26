using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Players.UpdatePlayer;

public class GivenPartialUpdate : GivenUpdatePlayerRequest
{
    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) OriginalData =
        ("Original Name", "OrigNick", false, false);

    private const string UpdatedName = "Updated Name";

    [OneTimeSetUp]
    public void SetupPlayer()
    {
        // Create a player to partially update
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var player = new PlayerBuilder()
            .WithName(OriginalData.Name)
            .WithNickname(OriginalData.Nickname)
            .WithCanDrive(OriginalData.CanDrive)
            .WithIsDeleted(OriginalData.IsDeleted)
            .Build();

        context.Players.Add(player);
        context.SaveChanges();
        _playerId = player.Id;
    }

    [OneTimeSetUp]
    public async Task WhenPlayerPartiallyUpdated()
    {
        // Update only the name, leave nickname as null
        await PutPlayer(_playerId, UpdatedName, null, OriginalData.CanDrive, OriginalData.IsDeleted);
    }

    [Test]
    public void ThenResponseStatusOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenOnlyUpdatedFieldsChanged()
    {
        // Verify name was updated but nickname stayed the same
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var player = context.Players.FirstOrDefault(p => p.Id == _playerId);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(player, Is.Not.Null);
            Assert.That(player!.Name, Is.EqualTo(UpdatedName), "Name should be updated");
            Assert.That(player.Nickname, Is.EqualTo(OriginalData.Nickname), "Nickname should remain unchanged");
            Assert.That(player.CanDrive, Is.EqualTo(OriginalData.CanDrive), "CanDrive should remain unchanged");
        }
    }
}