var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.Server>("webapi")
    //todo re-add swagger link when available
    .WithUrlForEndpoint("https", url => url.DisplayText = "Base (HTTPS)")
    .WithUrlForEndpoint("http", url => url.DisplayText = "Base (HTTP)")
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
