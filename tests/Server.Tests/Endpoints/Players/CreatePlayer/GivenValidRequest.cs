using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Server.Tests.Assertions;
using Skittles.WebApi.Infrastructure.Persistence;
using System.Net;
using System.Text.Json;

namespace Skittles.Server.Tests.Endpoints.Players.CreatePlayer;

public class GivenValidRequest : GivenCreatePlayerRequest
{
    private static readonly (string Name, string Nickname, bool CanDrive, bool IsDeleted) PlayerData =
        ("John Doe", "Johnny", true, false);

    private Guid _createdPlayerId;

    [OneTimeSetUp]
    public async Task WhenPlayerCreated()
    {
        await PostPlayer(PlayerData.Name, PlayerData.Nickname, PlayerData.CanDrive, PlayerData.IsDeleted);
        
        if (_response!.StatusCode == HttpStatusCode.OK)
        {
            var createResponse = JsonSerializer.Deserialize<CreatePlayerResponse>(_node!.ToJsonString(), JsonSerializerOptions);
            _createdPlayerId = createResponse?.Id ?? Guid.Empty;
        }
    }

    [Test]
    public void ThenResponseStatusOk()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public void ThenResponseContainsCreatedPlayerId()
    {
        Assert.That(_createdPlayerId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(_node!["id"]!.GetValue<Guid>(), Is.EqualTo(_createdPlayerId));
    }

    [Test]
    public void ThenPlayerExistsInDatabase()
    {
        // Query the database directly to verify the player was created
        using var scope = Factory!.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<SkittlesDbContext>();
        PlayerAssertions.AssertPlayerInDb(context, _createdPlayerId, PlayerData);
    }

    private record CreatePlayerResponse(Guid Id);
}
