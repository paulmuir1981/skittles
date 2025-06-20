var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.Server>("webapi")
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = "Swagger (HTTPS)";
        url.Url = "/swagger";
    })
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Swagger (HTTP)";
        url.Url = "/swagger";
    });

builder.AddProject<Projects.Client>("blazor")
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = "Home (HTTPS)";
        url.Url = "/";
    })
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Home (HTTP)";
        url.Url = "/";
    })
    .WithExternalHttpEndpoints()
    .WithReference(webapi)
    .WaitFor(webapi);

using var app = builder.Build();

await app.RunAsync();
