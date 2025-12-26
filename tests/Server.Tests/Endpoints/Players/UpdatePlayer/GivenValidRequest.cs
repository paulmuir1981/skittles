using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Assertions;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Players.UpdatePlayer;

public class GivenValidRequest : GivenUpdatePlayerRequest
{
    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) OriginalData =
        ("John Doe", "Johnny", true, false);

    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) UpdatedData =
        ("Jane Doe", "Jane", false, false);

    [OneTimeSetUp]
    public void SetupPlayer()
    {
        // Create a player to update
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
    public async Task WhenPlayerUpdated()
    {
        await PutPlayer(_playerId, UpdatedData.Name, UpdatedData.Nickname, UpdatedData.CanDrive, UpdatedData.IsDeleted);
    }

    [Test]
    public void ThenResponseStatusOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResponseContainsPlayerId()
    {
        Assert.That(_node!["id"]!.GetValue<Guid>(), Is.EqualTo(_playerId));
    }

    [Test]
    public void ThenPlayerUpdatedInDatabase()
    {
        // Query the database to verify the player was updated
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();
        PlayerAssertions.AssertPlayerInDb(context, _playerId, UpdatedData);
    }
}