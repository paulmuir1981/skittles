using Aspire.Hosting;
using Microsoft.Extensions.Logging;

namespace Skittles.Host.Tests;

public class EnvironmentVariableValuesTest : HostTestBase
{
    [Test]
    public async Task GetEnvironmentVariableValues_ResolveToWebApiService()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Host>();

        var frontend = builder.CreateResourceBuilder<ProjectResource>("blazor");

        var serviceProvider = builder.Services.BuildServiceProvider();
        var executionContextOptions = new DistributedApplicationExecutionContextOptions(DistributedApplicationOperation.Publish)
        {
            ServiceProvider = serviceProvider
        };
        var executionContext = new DistributedApplicationExecutionContext(executionContextOptions);

        var logger = serviceProvider.GetRequiredService<ILogger<ExecutionConfigurationBuilder>>();
        var resolvedConfiguration = await ExecutionConfigurationBuilder
            .Create(frontend.Resource)
            .WithEnvironmentVariablesConfig()
            .BuildAsync(executionContext, logger);

        var envVars = resolvedConfiguration.EnvironmentVariables;

        Assert.That(envVars, Does.Contain(
            new KeyValuePair<string, string>(
                key: "services__webapi__https__0",
                value: "{webapi.bindings.https.url}")));
    }
}