using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Host;
using Skittles.WebApi.Infrastructure.Persistence;

namespace Skittles.WebApi.Tests;

[Category("Server")]
public abstract class ServerTestBase
{
    protected WebApplicationFactory<Program>? Factory;
    protected HttpClient? HttpClient;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove ALL DbContext registrations (SQL Server, any existing ones)
                    var dbDescriptors = services.Where(d =>
                        d.ServiceType.IsGenericType &&
                        d.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>) ||
                        d.ServiceType == typeof(DbContextOptions<SkittlesDbContext>) ||
                        d.ServiceType.Name.Contains("DbContextOptions")).ToList();

                    foreach (var descriptor in dbDescriptors)
                    {
                        services.Remove(descriptor);
                    }

                    // Add ONLY in-memory database
                    services.AddDbContext<SkittlesDbContext>(options =>
                        options.UseInMemoryDatabase("SkittlesIntegrationTest"), ServiceLifetime.Scoped);

                    // Replace initializer  
                    var initDescriptor = services.SingleOrDefault(d =>
                        d.ServiceType == typeof(IDbInitializer));

                    if (initDescriptor is not null)
                    {
                        services.Remove(initDescriptor);
                    }

                    services.AddScoped<IDbInitializer, TestDbInitializer>();
                });
            });

        HttpClient = Factory.CreateClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Factory?.Dispose();
        HttpClient?.Dispose();
    }

    protected async Task<T?> GetJsonResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return System.Text.Json.JsonSerializer.Deserialize<T>(content, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    private class TestDbInitializer : IDbInitializer
    {
        private readonly SkittlesDbContext context;

        public TestDbInitializer(SkittlesDbContext context)
        {
            this.context = context;
        }

        public async Task MigrateAsync(CancellationToken cancellationToken)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        public async Task SeedAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}