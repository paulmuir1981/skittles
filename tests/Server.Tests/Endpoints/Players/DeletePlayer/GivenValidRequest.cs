using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Assertions;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Players.DeletePlayer;

public class GivenValidRequest : GivenDeletePlayerRequest
{
    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) PlayerData =
        ("John Doe", "Johnny", true, false);

    [OneTimeSetUp]
    public void SetupPlayer()
    {
        // Create a player to delete
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var player = new PlayerBuilder()
            .WithName(PlayerData.Name)
            .WithNickname(PlayerData.Nickname)
            .WithCanDrive(PlayerData.CanDrive)
            .WithIsDeleted(PlayerData.IsDeleted)
            .Build();

        context.Players.Add(player);
        context.SaveChanges();
        _playerId = player.Id;
    }

    [OneTimeSetUp]
    public async Task WhenPlayerDeleted()
    {
        await DeletePlayer(_playerId);
    }

    [Test]
    public void ThenResponseStatusNoContent()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public void ThenResponseHasNoContent()
    {
        Assert.That(_response!.Content.ReadAsStringAsync().Result, Is.Empty);
    }

    [Test]
    public void ThenPlayerRemovedFromDatabase()
    {
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();
        PlayerAssertions.AssertPlayerDeletedFromDb(context, _playerId);
    }
}