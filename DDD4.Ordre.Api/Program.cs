using DDD4.Order.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.Configure<OrdersMongoDbSettings>(
    builder.Configuration.GetSection("OrderDocuments"));

app.Run();
