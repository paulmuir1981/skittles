using Skittles.Framework.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSkittlesFramework();
var app = builder.Build();

app.UseSkittlesFramework();

app.MapGet("/players", () =>
{
    var players = Enumerable.Range(1, 15).Select(index =>
    {
        var playerId = Guid.NewGuid();
        var player = new Player
        (
            playerId,
            $"Player {index}",
            [], 
            []
        );
        return player;
    });
    return players;
})
.WithName("GetPlayers");

app.MapDefaultEndpoints();

app.Run();

record Player(Guid Id, string Name, IReadOnlyList<Score> Scores, IReadOnlyList<Registration> Registrations);
record Season(Guid Id, int Year, IReadOnlyList<Week> Weeks, IReadOnlyList<Registration> Registrations);
record Registration(Guid PlayerId, Guid SeasonId);
record Week(Guid Id, Guid SeasonId, int Number, DateOnly Date, bool IsPractice, IReadOnlyList<Leg> Legs);
record Leg(Guid Id, int Number, int WeekId, IReadOnlyList<Score> Scores);
record Score(Guid Id, Guid LegId, Guid PlayerId, int Value);
