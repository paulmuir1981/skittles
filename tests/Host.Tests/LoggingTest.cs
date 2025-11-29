using Microsoft.Extensions.Logging;

namespace Skittles.Aspire.Tests;

[Category("Aspire")]
public class LoggingTest
{
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

    [Test]
    public async Task GetAsync_BlazorRoot_ReturnsOkStatusCodeWithLogging()
    {
        // Arrange
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Host>();

        builder.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        // Configure logging to capture test execution logs  
        builder.Services.AddLogging(logging => logging
            .AddConsole() // Outputs logs to console
            .AddFilter("Default", LogLevel.Information)
            .AddFilter("Microsoft.AspNetCore", LogLevel.Warning)
            .AddFilter("Aspire.Hosting.Dcp", LogLevel.Warning));

        await using var app = await builder.BuildAsync();

        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("blazor");

        using var cts = new CancellationTokenSource(DefaultTimeout);
        await app.ResourceNotifications.WaitForResourceHealthyAsync(
                "blazor",
                cts.Token);

        var response = await httpClient.GetAsync("/");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}