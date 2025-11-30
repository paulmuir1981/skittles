using System.Net;
using System.Text;
using System.Text.Json;
using NUnit.Framework;
using Skittles.WebApi.Application.Players.Create.v1;
using Skittles.WebApi.Application.Players.Update.v1;

namespace Skittles.WebApi.Tests.Endpoints;

public class PlayersIntegrationTests : ServerTestBase
{
    private Guid _testPlayerId;

    [Test, Order(1)]
    public async Task CreatePlayer_WithValidData_ReturnsCreated()
    {
        // Arrange
        var request = new CreatePlayerRequest("John Doe", "Johnny", CanDrive: true, IsDeleted: false);
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await HttpClient!.PostAsync("/api/v1/players", content);
        var result = await GetJsonResponse<CreatePlayerResponse>(response);
        _testPlayerId = result?.Id ?? Guid.Empty;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(_testPlayerId, Is.Not.EqualTo(Guid.Empty));
    }

    [Test, Order(2)]
    public async Task GetPlayer_WithValidId_ReturnsPlayer()
    {
        // Act
        var response = await HttpClient!.GetAsync($"/api/v1/players/{_testPlayerId}");
        var jsonContent = await response.Content.ReadAsStringAsync();
        
        // Use case-insensitive deserialization
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var player = JsonSerializer.Deserialize<dynamic>(jsonContent, options);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Response: {jsonContent}");
        Assert.That(jsonContent, Does.Contain("John Doe"), "Response should contain player name");
    }

    [Test, Order(3)]
    public async Task ListPlayers_ReturnsAllPlayers()
    {
        // Act
        var response = await HttpClient!.GetAsync("/api/v1/players?includeDeleted=false");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var players = await GetJsonResponse<List<dynamic>>(response);
        Assert.That(players, Is.Not.Null);
        Assert.That(players?.Count, Is.GreaterThan(0));
    }

    [Test, Order(4)]
    public async Task UpdatePlayer_WithValidData_ReturnsUpdated()
    {
        // Arrange
        var request = new UpdatePlayerRequest
        (
            Name: "Jane Doe",
            Nickname: "Jane",
            CanDrive: false,
            IsDeleted: false
        );
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await HttpClient!.PutAsync($"/api/v1/players/{_testPlayerId}", content);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test, Order(5)]
    public async Task DeletePlayer_WithValidId_ReturnsNoContent()
    {
        // Act
        var response = await HttpClient!.DeleteAsync($"/api/v1/players/{_testPlayerId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task GetPlayer_WithInvalidId_ReturnsNotFound()
    {
        // Act
        var response = await HttpClient!.GetAsync($"/api/v1/players/{Guid.NewGuid()}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }
}