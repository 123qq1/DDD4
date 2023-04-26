using DDD4.Customer.Infrastructure.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CustomersMongoDbSettings>(
    builder.Configuration.GetSection("CustomerDocuments"));

var app = builder.Build();



app.Run();
