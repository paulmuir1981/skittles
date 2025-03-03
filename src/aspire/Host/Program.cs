var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var webapi = builder.AddProject<Projects.Skittles_ApiService>("webapi");

//builder.AddProject<Projects.Skittles_Web>("blazor")
//    .WithExternalHttpEndpoints()
//    .WithReference(cache)
//    .WaitFor(cache)
//    .WithReference(webapi)
//    .WaitFor(webapi);

builder.AddProject<Projects.Client>("client")
    .WithExternalHttpEndpoints()
//    .WithReference(cache)
//    .WaitFor(cache)
    .WithReference(webapi)
    .WaitFor(webapi);

using var app = builder.Build();

await app.RunAsync();
