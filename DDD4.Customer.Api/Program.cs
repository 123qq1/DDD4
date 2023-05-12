using System.Reflection;
using DDD4.Customer.Application.CQRS.Commands.CreateCustomer;
using DDD4.Customer.Application.Repositories;
using DDD4.Customer.Infrastructure.Config;
using DDD4.Customer.Infrastructure.Consumers;
using DDD4.Customer.Infrastructure.Repositories;
using DDD4.Customer.Application.CQRS.Queries;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();

builder.Services.AddEventStoreClient(
    new Uri(
        builder.Configuration.GetSection("EventStore").Get<string>()
    )
);

builder.Services.Configure<CustomersMongoDbSettings>(
    builder.Configuration.GetSection("CustomerDocuments"));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssemblyContaining<CreateCustomerCommand>();
    cfg.RegisterServicesFromAssemblyContaining<CreateCustomerConsumer>();
    cfg.RegisterServicesFromAssemblyContaining<CreatedCustomerHandler>();
    cfg.RegisterServicesFromAssemblyContaining<ReadCustomersHandler>();
});

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
