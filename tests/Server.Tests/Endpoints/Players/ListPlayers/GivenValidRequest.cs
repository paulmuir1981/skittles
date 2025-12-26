using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Assertions;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Players.ListPlayers;

[TestFixture(false, 1)]
[TestFixture(true, 2)]
public class GivenValidRequest : ServerTestBase
{
    private Guid _playerId1;
    private Guid _playerId2;

    private readonly bool _includeDeleted;
    private readonly int _expectedCount;
    private HttpResponseMessage? _response;
    private JsonNode? _node;

    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) Player1Data =
        ("I am a name", "I am a nickname", true, false);

    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) Player2Data =
        ("I am another name", "I am another nickname", true, true);

    public GivenValidRequest(bool includeDeleted, int expectedCount)
    {
        _includeDeleted = includeDeleted;
        _expectedCount = expectedCount;
    }

    [OneTimeSetUp]
    public async Task WhenRequested()
    {
        _response = await HttpClient!.GetAsync($"/api/v1/players?includeDeleted={_includeDeleted}");
        var json = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(json)!;
    }

    protected override void SetupSkittlesDbContext()
    {
        using var scope = Factory!.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var player1 = new PlayerBuilder()
            .WithName(Player1Data.Name)
            .WithNickname(Player1Data.Nickname)
            .WithCanDrive(Player1Data.CanDrive)
            .WithIsDeleted(Player1Data.IsDeleted)
            .Build();

        var player2 = new PlayerBuilder()
            .WithName(Player2Data.Name)
            .WithNickname(Player2Data.Nickname)
            .WithCanDrive(Player2Data.CanDrive)
            .WithIsDeleted(Player2Data.IsDeleted)
            .Build();

        ctx.Players.Add(player1);
        ctx.Players.Add(player2);
        ctx.SaveChanges();

        _playerId1 = player1.Id;
        _playerId2 = player2.Id;
    }

    [Test]
    public void ThenResponseStatusOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResponseMediaTypeApplicationJson()
    {
        Assert.That(_response!.Content.Headers.ContentType!.MediaType, Is.EqualTo("application/json"));
    }

    [Test]
    public void ThenResultExpected()
    {
        Assert.That(_node, Has.Exactly(_expectedCount).Items);

        var sorted = ((JsonArray)_node!).OrderBy(x => (bool?)x!["isDeleted"]).ToList();

        PlayerAssertions.AssertPlayerResponse(sorted[0]!, _playerId1, Player1Data);

        if (_includeDeleted)
        {
            PlayerAssertions.AssertPlayerResponse(sorted[1]!, _playerId2, Player2Data);
        }
    }
}
