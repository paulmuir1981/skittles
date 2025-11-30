using Aspire.Hosting;

namespace Skittles.Aspire.Tests;

public class EnvironmentVariableValuesTest : AspireTestBase
{
    [Test]
    public async Task GetEnvironmentVariableValues_ResolveToWebApiService()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Host>();

        var frontend = builder.CreateResourceBuilder<ProjectResource>("blazor");

        var envVars = await frontend.Resource.GetEnvironmentVariableValuesAsync(
            DistributedApplicationOperation.Publish);

        Assert.That(envVars, Does.Contain(
            new KeyValuePair<string, string>(
                key: "services__webapi__https__0",
                value: "{webapi.bindings.https.url}")));
    }
}