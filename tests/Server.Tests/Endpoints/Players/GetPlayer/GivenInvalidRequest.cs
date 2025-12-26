using NUnit.Framework;
using System.Net;

namespace Skittles.Server.Tests.Endpoints.Players.GetPlayer;

public class GivenInvalidRequest : GivenGetPlayerRequest
{
    [OneTimeSetUp]
    public new async Task WhenRequested()
    {
        _fakeId = Guid.NewGuid();
        await base.WhenRequested();
    }

    [Test]
    public void ThenResponseStatusNotFound()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public void ThenResultExpected()
    {
        Assert.That(_node, Has.Exactly(2).Items);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(
                _node!["detail"]!.GetValue<string>(), 
                Is.EqualTo($"player with id {_fakeId} not found"));
            Assert.That(
                _node!["instance"]!.GetValue<string>(), 
                Is.EqualTo($"/api/v1/players/{_fakeId}"));
        }
    }
}
