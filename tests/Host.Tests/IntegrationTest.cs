using Microsoft.Extensions.Logging;

namespace Skittles.Aspire.Tests;

public class IntegrationTest : AspireTestBase
{
    protected override void ConfigureLogging(IDistributedApplicationTestingBuilder builder)
    {
        builder.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Debug);
            logging.AddConsole();
            logging.AddFilter(builder.Environment.ApplicationName, LogLevel.Debug);
            logging.AddFilter("Microsoft.AspNetCore", LogLevel.Debug);
            logging.AddFilter("Aspire", LogLevel.Debug);
        });
    }

    [Test]
    public async Task GetAsync_BlazorRoot_ReturnsOkStatusCode()
    {
        var response = await HttpClient!.GetAsync("/");

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
