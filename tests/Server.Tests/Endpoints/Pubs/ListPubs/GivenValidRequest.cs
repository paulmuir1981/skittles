using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Pubs.ListPubs;

[TestFixture(null, 1)]
[TestFixture(false, 1)]
[TestFixture(true, 2)]
public class GivenValidRequest : GivenListPubsRequest
{
    private Guid _pubId1;
    private Guid _pubId2;

    private readonly bool? _includeDeleted;
    private readonly int _expectedCount;

    private static readonly (string Name, string Postcode, bool IsDeleted) Pub1Data = (
        "The Red Lion",
        "SW1A 1AA",
        false);

    private static readonly (string Name, string Postcode, bool IsDeleted) Pub2Data = (
        "The Crown",
        "SW1A 2AA",
        true);

    public GivenValidRequest(bool? includeDeleted, int expectedCount)
    {
        _includeDeleted = includeDeleted;
        _expectedCount = expectedCount;
    }

    [OneTimeSetUp]
    public async Task WhenPubsCreated()
    {
        using var scope = Factory!.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var pub1 = Pub.Create(Pub1Data.Name, Pub1Data.Postcode, Pub1Data.IsDeleted);
        var pub2 = Pub.Create(Pub2Data.Name, Pub2Data.Postcode, Pub2Data.IsDeleted);

        dbContext.Pubs.AddRange(pub1, pub2);
        await dbContext.SaveChangesAsync();

        _pubId1 = pub1.Id;
        _pubId2 = pub2.Id;

        await GetPubs(_includeDeleted ?? false);
    }

    [Test]
    public void ThenResponseStatusCodeOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResponseContainsExpectedNumberOfPubs()
    {
        var pubs = _node!.AsArray();
        Assert.That(pubs.Count, Is.EqualTo(_expectedCount));
    }

    [Test]
    public void ThenResponseContainsCorrectPubData()
    {
        var sorted = ((JsonArray)_node!).OrderBy(x => (bool?)x!["isDeleted"]).ToList();
        //var pubs = _node!.AsArray();
        var firstPub = sorted[0]!.AsObject();

        Assert.That(firstPub["name"]?.GetValue<string>(), Is.EqualTo(Pub1Data.Name));
        Assert.That(firstPub["postcode"]?.GetValue<string>(), Is.EqualTo(Pub1Data.Postcode));
        Assert.That(firstPub["isDeleted"]?.GetValue<bool>(), Is.EqualTo(Pub1Data.IsDeleted));
    }
}
