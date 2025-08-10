var builder = DistributedApplication.CreateBuilder(args);

//var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.StriderNeighbours_ApiService>("apiservice")
    .WithExternalHttpEndpoints()
    .WithEndpoint("http", endpoint => endpoint.IsProxied = false);

builder.AddProject<Projects.StriderNeighbours_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    //.WithReference(cache)
    //.WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithEndpoint("http", endpoint => endpoint.IsProxied = false);

builder.Build().Run();
