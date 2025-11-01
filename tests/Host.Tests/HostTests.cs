using Microsoft.Extensions.Logging;

namespace Skittles.Aspire.Tests;

public abstract class HostTests
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

    private readonly string _resourceName;
    private readonly string? _requestUri;
    private readonly HttpStatusCode _expectedCode;
    private readonly string? _expectedContentType;

    protected HostTests(string resourceName, string? requestUri, HttpStatusCode expectedCode, string? expectedContentType)
    {
        _resourceName = resourceName;
        _requestUri = requestUri;
        _expectedCode = expectedCode;
        _expectedContentType = expectedContentType;
    }

    [Test]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        using var cts = new CancellationTokenSource(DefaultTimeout);
        var cancellationToken = cts.Token;
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.Host>();
        appHost.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Debug);
            // Override the logging filters from the app's configuration
            logging.AddFilter(appHost.Environment.ApplicationName, LogLevel.Debug);
            logging.AddFilter("Aspire.", LogLevel.Debug);
        });
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        await using var app = await appHost.BuildAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
        await app.StartAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);

        using var httpClient = app.CreateHttpClient(_resourceName);
        await app.ResourceNotifications.WaitForResourceHealthyAsync(_resourceName, cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
        using var response = await httpClient.GetAsync(_requestUri, cancellationToken);

        Assert.That(response.StatusCode, Is.EqualTo(_expectedCode));

        var contentType = response.Content.Headers.ContentType;

        if (_expectedContentType == null)
        {
            Assert.That(contentType, Is.Null);
            return;
        }

        Assert.That(contentType, Is.Not.Null);
        var mediaType = contentType!.MediaType;
        Assert.That(mediaType, Is.EqualTo(_expectedContentType));
    }
}
