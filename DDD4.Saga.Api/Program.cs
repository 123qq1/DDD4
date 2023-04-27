using DDD4.Saga.Components.StateMachines;
using DDD4.Saga.DbContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(cfg =>
{
    cfg.SetEntityFrameworkSagaRepositoryProvider(r =>
    {
        r.ConcurrencyMode = ConcurrencyMode.Pessimistic;

        r.ExistingDbContext<EntityFrameworkDbContext>();

        r.UseSqlServer();
    });

    cfg.AddSagaStateMachinesFromNamespaceContaining<StateMachineAnchor>();
    cfg.AddSagasFromNamespaceContaining<StateMachineAnchor>();

    cfg.AddDelayedMessageScheduler();
    cfg.UsingRabbitMq((x, y) =>
    {
        y.UseDelayedMessageScheduler();

        y.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        y.ConfigureEndpoints(x);
    });

});
var connectionString = builder.Configuration.GetConnectionString("sagaDb");
builder.Services.AddDbContext<EntityFrameworkDbContext>(
    x => x.UseSqlServer(connectionString, b => b.MigrationsAssembly("DDD4.Saga.DbContext"))
    );

var app = builder.Build();

app.Run();
