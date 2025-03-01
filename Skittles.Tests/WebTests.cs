using Aspire.Hosting;

namespace Skittles.Tests;

public class WebTests : IDisposable
{
    private DistributedApplication _app;
    private HttpClient _httpClient;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Skittles_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        _app = await appHost.BuildAsync();
        var resourceNotificationService = _app.Services.GetRequiredService<ResourceNotificationService>();
        await _app.StartAsync();
        _httpClient = _app.CreateHttpClient("webfrontend");
        await resourceNotificationService.WaitForResourceAsync("webfrontend", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Dispose();
    }

    [Test]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        //// Act
        var response = await _httpClient.GetAsync("/");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetWebResourcePlayersReturnsOkStatusCode()
    {
        // Act
        var response = await _httpClient.GetAsync("/players");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetWebResourceRandomUriReturnsNotFoundStatusCode()
    {
        // Act
        var response = await _httpClient.GetAsync($"/{Guid.NewGuid}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
        _app?.Dispose();
        GC.SuppressFinalize(this);
    }
}
