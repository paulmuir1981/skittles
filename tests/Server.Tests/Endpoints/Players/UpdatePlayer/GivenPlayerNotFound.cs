using NUnit.Framework;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Players.UpdatePlayer;

public class GivenPlayerNotFound : GivenUpdatePlayerRequest
{
    [OneTimeSetUp]
    public async Task WhenUpdatingNonExistentPlayer()
    {
        var nonExistentId = Guid.NewGuid();
        await PutPlayer(nonExistentId, "Updated Name", "UpdatedNick", true, false);
    }

    [Test]
    public void ThenResponseStatusNotFound()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void ThenResponseContainsErrorDetails()
    {
        Assert.That(_node, Is.Not.Null);
        Assert.That(
            _node!["detail"]!.GetValue<string>(),
            Does.Contain("not found"));
    }
}