using Skittles.Framework.Infrastructure;
using Skittles.WebApi.Server;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSkittlesFramework();
builder.RegisterModules();
var app = builder.Build();

app.UseSkittlesFramework();
app.UseModules();

app.MapDefaultEndpoints();

app.Run();