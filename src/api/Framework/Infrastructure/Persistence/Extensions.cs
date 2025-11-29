using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Skittles.Framework.Core.Persistence;
using Skittles.Framework.Infrastructure.Persistence.Interceptors;

namespace Skittles.Framework.Infrastructure.Persistence;

public static class Extensions
{
    private const string MemoryConnectionString = "Data Source=:memory:";

    internal static DbContextOptionsBuilder ConfigureDatabase(this DbContextOptionsBuilder builder, string connectionString)
    {
        if (connectionString == MemoryConnectionString)
        {
            builder.UseSqlite(connectionString, e => e.MigrationsAssembly("Skittles.WebApi.Migrations"));
        }
        else
        {
            builder.UseSqlServer(connectionString, e => e.MigrationsAssembly("Skittles.WebApi.Migrations"));
        }
        return builder;
    }

    public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddOptions<DatabaseOptions>()
            .BindConfiguration(nameof(DatabaseOptions))
            .ValidateDataAnnotations();
        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        return builder;
    }

    public static IServiceCollection BindDbContext<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<TContext>((sp, options) =>
        {
            var dbConfig = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.ConfigureDatabase(dbConfig.ConnectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });
        return services;
    }

    public static IApplicationBuilder UseDatabase(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        using var scope = app.ApplicationServices.CreateScope();
        var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();
        foreach (var initializer in initializers)
        {
            initializer.MigrateAsync(CancellationToken.None).Wait();
            initializer.SeedAsync(CancellationToken.None).Wait();
        }
        return app;
    }
}
