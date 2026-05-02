using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text.Json;

namespace Skittles.Server.Tests.Endpoints.Pubs.CreatePub;

public class GivenValidRequest : GivenCreatePubRequest
{
    private static readonly (string Name, string Postcode, bool IsDeleted) PubData = (
        "The Red Lion",
        "SW1A 1AA",
        false);

    private Guid _createdPubId;

    [OneTimeSetUp]
    public async Task WhenPubCreated()
    {
        await PostPub(
            PubData.Name,
            PubData.Postcode,
            PubData.IsDeleted);

        if (_response!.StatusCode == HttpStatusCode.OK)
        {
            var createResponse = JsonSerializer.Deserialize<CreatePubResponse>(_node!.ToJsonString(), JsonSerializerOptions);
            _createdPubId = createResponse?.Id ?? Guid.Empty;
        }
    }

    [Test]
    public void ThenResponseStatusCodeOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResponseBodyContainsId()
    {
        var id = _node!["id"]?.GetValue<string>();
        Assert.That(id, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task ThenPubIsCreatedInDatabase()
    {
        using var scope = Factory!.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();
        var pub = await dbContext.Pubs.FindAsync(_createdPubId);
        Assert.That(pub, Is.Not.Null);
        Assert.That(pub.Name, Is.EqualTo(PubData.Name));
        Assert.That(pub.Postcode, Is.EqualTo(PubData.Postcode));
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }

    private record CreatePubResponse(Guid Id);
}
