var builder = DistributedApplication.CreateBuilder(args);

var webapi = builder.AddProject<Projects.Server>("webapi");

builder.AddProject<Projects.Client>("blazor")
    .WithExternalHttpEndpoints()
    .WithReference(webapi)
    .WaitFor(webapi);

using var app = builder.Build();

await app.RunAsync();
