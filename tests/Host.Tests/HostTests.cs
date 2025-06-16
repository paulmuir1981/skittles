using Aspire.Hosting;

namespace Skittles.Aspire.Tests;

public abstract class HostTests : IDisposable
{
    private DistributedApplication _app;
    private HttpClient? _client;
    private readonly string _resourceName;
    private readonly string? _requestUri;
    private readonly HttpStatusCode _expectedCode;
    private readonly string? _expectedContentType;
    protected HttpResponseMessage? _response;

    protected HostTests(string resourceName, string? requestUri, HttpStatusCode expectedCode, string? expectedContentType)
    {
        _resourceName = resourceName;
        _requestUri = requestUri;
        _expectedCode = expectedCode;
        _expectedContentType = expectedContentType;
    }

    [OneTimeSetUp]
    public async Task WhenRequested()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Host>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            //todo uncomment
            //clientBuilder.AddStandardResilienceHandler();
        });

        _app = await appHost.BuildAsync();
        var resourceNotificationService = _app.Services.GetRequiredService<ResourceNotificationService>();
        await _app.StartAsync();
        await resourceNotificationService.WaitForResourceAsync("blazor", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromMinutes(1));
        _client = _app.CreateHttpClient(_resourceName);

        _response = await _client.GetAsync(_requestUri);
    }

    [Test]
    public void ThenResponseHasExpectedStatusCode()
    {
        Assert.That(_response!.StatusCode, Is.EqualTo(_expectedCode));
    }

    [Test]
    public void ThenContentTypeExpected()
    {
        var contentType = _response!.Content.Headers.ContentType;

        if(_expectedContentType == null)
        {
            Assert.That(contentType, Is.Null);
            return;
        }

        Assert.That(contentType, Is.Not.Null);
        var mediaType = contentType!.MediaType;
        Assert.That(mediaType, Is.EqualTo(_expectedContentType));
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Dispose();
    }

    public void Dispose()
    {
        _response?.Dispose();
        _client?.Dispose();
        _app.Dispose();
        GC.SuppressFinalize(this);
    }
}
