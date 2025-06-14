using Aspire.Hosting;

namespace Skittles.Aspire.Tests;

public class HostTests : IDisposable
{
    private DistributedApplication _app;
    private HttpClient _blazorClient;
    private HttpClient _webapiClient;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Host>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        _app = await appHost.BuildAsync();
        var resourceNotificationService = _app.Services.GetRequiredService<ResourceNotificationService>();
        await _app.StartAsync();
        _blazorClient = _app.CreateHttpClient("blazor");
        _webapiClient = _app.CreateHttpClient("webapi");
        await resourceNotificationService.WaitForResourceAsync("blazor", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));
    }

    [Test]
    public async Task GetBlazorKnownUriReturnsOkStatusCode([Values("/", "/players")] string requestUri)
    {
        // Act
        var response = await _blazorClient.GetAsync(requestUri);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetWebApiKnownUriReturnsOkStatusCode([Values("/swagger", "/swagger/index.html", "/api/v1/players")] string requestUri)
    {
        // Act
        var response = await _webapiClient.GetAsync(requestUri);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetWebApiRandomUriReturnsNotFoundStatusCode()
    {
        // Act
        var response = await _webapiClient.GetAsync($"/{Guid.NewGuid()}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetWebApiBlankUriReturnsNotFoundStatusCode([Values(null, "", "/")] string? requestUri)
    {
        // Act
        var response = await _webapiClient.GetAsync(requestUri);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetWebApiUndefinedVersionReturnsNotFoundStatusCode([Values(0, 2, int.MaxValue)] int version)
    {
        // Act
        var response = await _webapiClient.GetAsync($"/api/v{version}/players");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Dispose();
    }

    public void Dispose()
    {
        _blazorClient?.Dispose();
        _app?.Dispose();
        GC.SuppressFinalize(this);
    }
}
