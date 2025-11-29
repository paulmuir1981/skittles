using Microsoft.Extensions.Logging;

namespace Skittles.Aspire.Tests;

public class LoggingTest : AspireTestBase
{
    protected override void ConfigureLogging(IDistributedApplicationTestingBuilder builder)
    {
        builder.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Information);
            logging.AddConsole();
            logging.AddFilter(builder.Environment.ApplicationName, LogLevel.Information);
            logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning);
            logging.AddFilter("Aspire", LogLevel.Warning);
        });
    }

    [Test]
    public async Task GetAsync_BlazorRoot_ReturnsOkStatusCodeWithLogging()
    {
        var response = await HttpClient!.GetAsync("/");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}