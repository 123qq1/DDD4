using DDD4.Customer.Api.MongoDbConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CustomersMongoDbSettings>(
    builder.Configuration.GetSection("CustomerDocuments"));

var app = builder.Build();



app.Run();
