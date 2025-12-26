using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Assertions;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Players.GetPlayer;

public class GivenValidRequest : GivenGetPlayerRequest
{
    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) PlayerData =
        ("I am a name", "I am a nickname", true, true);

    protected override void SetupSkittlesDbContext()
    {
        using var scope = Factory!.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var player = new PlayerBuilder()
            .WithName(PlayerData.Name)
            .WithNickname(PlayerData.Nickname)
            .WithCanDrive(PlayerData.CanDrive)
            .WithIsDeleted(PlayerData.IsDeleted)
            .Build();

        ctx.Players.Add(player);
        ctx.SaveChanges();
        _fakeId = player.Id;
    }

    [Test]
    public void ThenResponseStatusOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResultExpected()
    {
        PlayerAssertions.AssertPlayerResponse(_node!, _fakeId, PlayerData);
    }
}
