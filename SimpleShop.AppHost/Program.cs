using Aspire.Hosting;


var builder = DistributedApplication.CreateBuilder(args);

var catalogDb = builder.AddPostgres("catalog").WithPgAdmin().AddDatabase("catalogdb");

var basketCache = builder.AddRedis("basketcache");

var cts = builder.AddProject<Projects.CatalogService>("catalogservice")
    .WithReference(catalogDb);

var bs = builder.AddProject<Projects.BasketService>("basketservice")
        .WithReference(basketCache);

builder.AddProject<Projects.Frontend>("frontend")
    .WithReference(cts)
    .WithReference(bs); 

builder.AddProject<Projects.CatalogDbManager>("catalogdbmanager")
    .WithReference(catalogDb);

builder.Build().Run();
