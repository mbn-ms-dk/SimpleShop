using BasketService.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRedis("basketcache");


builder.Services.AddGrpc();
builder.Services.AddGrpcHealthChecks();

builder.Services.AddTransient<IBasketRepository, RedisBasketRepository>();

var app = builder.Build();

app.MapGrpcService<BasketService.Services.BasketService>();

app.MapGrpcHealthChecksService();

app.MapDefaultEndpoints();


app.Run();
