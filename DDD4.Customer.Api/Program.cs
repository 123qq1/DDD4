using DDD4.Customer.Infrastructure.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(cfg =>
{
    cfg.AddDelayedMessageScheduler();
    cfg.UsingRabbitMq((x, y) =>
    {
        y.UseDelayedMessageScheduler();

        y.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        y.ConfigureEndpoints(x);
    });

    cfg.AddConsumersFromNamespaceContaining<CreateCustomerConsumer>();

});


var app = builder.Build();



app.Run();
