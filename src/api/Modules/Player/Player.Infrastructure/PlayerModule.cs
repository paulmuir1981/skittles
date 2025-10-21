using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.WebApi.Player.Domain;
using Skittles.WebApi.Player.Infrastructure.Endpoints.v1;

namespace Skittles.WebApi.Player.Infrastructure;
public static class PlayerModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base() { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var playerGroup = app.MapGroup("players").WithTags("players");
            playerGroup.MapListPlayersEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterPlayerServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        
        // Register DbContext
        builder.Services.AddDbContext<PlayerDbContext>(options =>
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SkittlesDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"));
        
        // Register repositories
        builder.Services.AddScoped<Skittles.WebApi.Player.Application.IPlayerRepository, PlayerRepository>();
        
        return builder;
    }
    public static WebApplication UsePlayerModule(this WebApplication app)
    {
        // Ensure database is created and seeded
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PlayerDbContext>();
            context.Database.EnsureCreated();
            
            var seeder = new PlayerDbSeeder(context);
            seeder.SeedAsync().Wait();
        }
        
        return app;
    }
}
