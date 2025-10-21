using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
using Skittles.Framework.Infrastructure.Persistence;
using Skittles.WebApi.Domain;
using Skittles.WebApi.Infrastructure.Endpoints.v1;

namespace Skittles.WebApi.Infrastructure;

public static class SkittlesModule
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

    public static WebApplicationBuilder RegisterSkittlesServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<SkittlesDbContext>();
        builder.Services.AddScoped<IDbInitializer, SkittlesDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<Player>, SkittlesRepository<Player>>("skittles:players");
        builder.Services.AddKeyedScoped<IReadRepository<Player>, SkittlesRepository<Player>>("skittles:players");
        return builder;
    }

    public static WebApplication UseSkittlesModule(this WebApplication app)
    {
        return app;
    }
}
