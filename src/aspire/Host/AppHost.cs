var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.Server>("webapi")
    .WithUrlForEndpoint("https", url => url.DisplayText = "Swagger (HTTPS)")
    .WithUrlForEndpoint("http", url => url.DisplayText = "Swagger (HTTP)")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Client>("blazor")
    .WithExternalHttpEndpoints()
    .WithUrlForEndpoint("https", url => url.DisplayText = "Home (HTTPS)")
    .WithUrlForEndpoint("http", url => url.DisplayText = "Home (HTTP)")
    .WithHttpHealthCheck("/health")
    .WithReference(webapi)
    .WaitFor(webapi);

using var app = builder.Build();

app.Run();
