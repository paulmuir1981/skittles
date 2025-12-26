using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Builders;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Skittles.Server.Tests.Endpoints.Players.CreatePlayer;

public class GivenDuplicateNickname : GivenCreatePlayerRequest
{
    private const string SharedNickname = "UniqueNick";

    [OneTimeSetUp]
    public void SetupExistingPlayer()
    {
        // Create a player with a specific nickname
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();

        var player = new PlayerBuilder()
            .WithName("First Player")
            .WithNickname(SharedNickname)
            .Build();

        context.Players.Add(player);
        context.SaveChanges();
    }

    [OneTimeSetUp]
    public async Task WhenPlayerCreatedWithDuplicateNickname()
    {
        var payload = new { name = "Second Player", nickname = SharedNickname, canDrive = true, isDeleted = false };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _response = await HttpClient!.PostAsync("/api/v1/players", content);
        var responseContent = await _response.Content.ReadAsStringAsync();
        _node = JsonNode.Parse(responseContent)!;
    }

    [Test]
    public void ThenResponseStatusConflict()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
    }

    [Test]
    public void ThenResponseContainsDuplicateNicknameError()
    {
        Assert.That(_node, Is.Not.Null);
        Assert.That(
            _node!["detail"]!.GetValue<string>(), 
            Does.Contain($"nickname '{SharedNickname}' already exists"));
    }
}