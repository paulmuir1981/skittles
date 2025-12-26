using NUnit.Framework;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Players.DeletePlayer;

public class GivenPlayerNotFound : GivenDeletePlayerRequest
{
    [OneTimeSetUp]
    public async Task WhenDeletingNonExistentPlayer()
    {
        var nonExistentId = Guid.NewGuid();
        await DeletePlayer(nonExistentId);
    }

    [Test]
    public void ThenResponseStatusNotFound()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void ThenResponseContainsErrorDetails()
    {
        var content = _response!.Content.ReadAsStringAsync().Result;
        Assert.That(content, Does.Contain("not found"));
    }
}