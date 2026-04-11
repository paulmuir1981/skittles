using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Domain;

namespace Skittles.WebApi.Infrastructure.Persistence;

internal sealed class SkittlesDbInitializer(
    ILogger<SkittlesDbInitializer> logger,
    SkittlesDbContext context) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Applied database migrations for skittles module");
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        // Check if players already exist
        if (await context.Seasons.AnyAsync(cancellationToken).ConfigureAwait(false))
        {
            return; // Database already seeded
        }

        // Create some sample players
        var players = new List<Player>
        {
            Player.Create("Alice Johnson"),
            Player.Create("Bob Smith"),
            Player.Create("Charlie Brown"),
            Player.Create("Diana Prince"),
            Player.Create("Eve Wilson"),
            Player.Create("Frank Miller"),
            Player.Create("Grace Hopper"),
            Player.Create("Henry Ford"),
            Player.Create("Ivy Chen"),
            Player.Create("Jack Sparrow")
        };
        context.Players.AddRange(players);

        var seasons = Enumerable.Range(2025, DateTime.UtcNow.Year - 2025).Select(year => Season.Create(year));
        context.Seasons.AddRange(seasons);

        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seeding default skittles data");
    }
}
