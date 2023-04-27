using DDD4.Customer.Infrastructure.Config;
using DDD4.Customer.Infrastructure.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CustomersMongoDbSettings>(
    builder.Configuration.GetSection("CustomerDocuments"));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddDelayedMessageScheduler();
    
    cfg.AddConsumersFromNamespaceContaining<CreateCustomerConsumer>();
    
    cfg.UsingRabbitMq((x, y) =>
    {
        y.UseDelayedMessageScheduler();

        y.Host("rabbit", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        y.ConfigureEndpoints(x);
    });
});

var app = builder.Build();

app.Run();
