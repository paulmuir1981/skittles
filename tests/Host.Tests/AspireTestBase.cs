using Aspire.Hosting;
using Microsoft.Extensions.Logging;

namespace Skittles.Aspire.Tests;

[Category("Aspire")]
public abstract class AspireTestBase
{
    protected static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);
    protected DistributedApplication? App;
    protected HttpClient? HttpClient;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Host>();

        builder.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        ConfigureLogging(builder);

        App = await builder.BuildAsync();
        await App.StartAsync();

        using var cts = new CancellationTokenSource(DefaultTimeout);
        await App.ResourceNotifications.WaitForResourceHealthyAsync("blazor", cts.Token);

        HttpClient = App.CreateHttpClient("blazor");
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (App is not null)
        {
            await App.DisposeAsync();
        }

        HttpClient?.Dispose();
    }

    protected virtual void ConfigureLogging(IDistributedApplicationTestingBuilder builder)
    {
        var minLevel = LogLevel.Information;

        builder.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(minLevel);
            logging.AddConsole();
            logging.AddFilter(builder.Environment.ApplicationName, minLevel);
            logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
            logging.AddFilter("Aspire", LogLevel.Warning);
        });
    }
}