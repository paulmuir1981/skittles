using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Skittles.Framework.Core.Persistence;
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
    public static WebApplicationBuilder RegisterCatalogServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        //builder.Services.BindDbContext<CatalogDbContext>();
        //builder.Services.AddScoped<IDbInitializer, CatalogDbInitializer>();
        //builder.Services.AddKeyedScoped<IRepository<Product>, CatalogRepository<Product>>("catalog:products");
        //builder.Services.AddKeyedScoped<IReadRepository<Product>, CatalogRepository<Product>>("catalog:products");
        return builder;
    }
    public static WebApplication UseCatalogModule(this WebApplication app)
    {
        return app;
    }
}
