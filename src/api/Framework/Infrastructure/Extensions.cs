using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skittles.Framework.Infrastructure.Cors;

namespace Skittles.Framework.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureSkittlesFramework(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.AddServiceDefaults();
        builder.Services.AddCorsPolicy(builder.Configuration);
        builder.Services.AddProblemDetails();
        return builder;
    }
    public static WebApplication UseSkittlesFramework(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseCorsPolicy();
        return app;
    }
}