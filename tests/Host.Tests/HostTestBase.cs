using Aspire.Hosting;
using Microsoft.Extensions.Logging;

namespace Skittles.Host.Tests;

[Category("Host")]
public abstract class HostTestBase
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
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            try
            {
                await App.DisposeAsync().AsTask().WaitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                // Log this — confirms it's a disposal hang, not something else
                Console.WriteLine("App.DisposeAsync() timed out after 30s");
            }
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