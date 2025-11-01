namespace Skittles.Aspire.Tests;

[TestFixture]
public class GivenARequestToListPlayers : GivenARequestToWebApi
{
    public GivenARequestToListPlayers()
        : base("/api/v1/players", "application/json") { }

    //[Test]
    //public async Task ThenContentIsExpected()
    //{
    //    using var contentStream = await _response!.Content.ReadAsStreamAsync();
    //    var players = await JsonSerializer.DeserializeAsync<List<TestPlayerResponse>>(contentStream);

    //    Assert.That(players, Is.Not.Null);
    //    Assert.That(players, Has.Exactly(10).Items);

    //    var ids = players.Select(x => x.Id).ToList();
    //    Assert.That(ids, Is.Unique);
    //    Assert.That(ids, Has.No.Member(Guid.Empty));

    //    //var names = players.Select(x => x.Name).ToList();
    //    //var index = 1;
    //    //foreach(var name in names)
    //    //{
    //    //    Assert.That(name, Is.EqualTo($"Player {index++}"));
    //    //}
    //}

    //private record TestPlayerResponse(
    //    [property: JsonPropertyName("id")] Guid Id, [property: JsonPropertyName("name")] string Name);
}
