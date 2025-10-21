using Microsoft.EntityFrameworkCore;
using Skittles.WebApi.Player.Domain;

namespace Skittles.WebApi.Player.Infrastructure;

public class PlayerDbSeeder
{
    private readonly PlayerDbContext _context;

    public PlayerDbSeeder(PlayerDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Check if players already exist
        if (await _context.Players.AnyAsync())
        {
            return; // Database already seeded
        }

        // Create some sample players
        var players = new List<Skittles.WebApi.Player.Domain.Player>
        {
            new Skittles.WebApi.Player.Domain.Player("Alice Johnson"),
            new Skittles.WebApi.Player.Domain.Player("Bob Smith"),
            new Skittles.WebApi.Player.Domain.Player("Charlie Brown"),
            new Skittles.WebApi.Player.Domain.Player("Diana Prince"),
            new Skittles.WebApi.Player.Domain.Player("Eve Wilson"),
            new Skittles.WebApi.Player.Domain.Player("Frank Miller"),
            new Skittles.WebApi.Player.Domain.Player("Grace Hopper"),
            new Skittles.WebApi.Player.Domain.Player("Henry Ford"),
            new Skittles.WebApi.Player.Domain.Player("Ivy Chen"),
            new Skittles.WebApi.Player.Domain.Player("Jack Sparrow")
        };

        _context.Players.AddRange(players);
        await _context.SaveChangesAsync();
    }
}
