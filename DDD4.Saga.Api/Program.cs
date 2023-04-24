using MassTransit;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(cfg =>
{
    cfg.AddDelayedMessageScheduler();
    cfg.UsingRabbitMq((x, y) =>
    {
        y.UseDelayedMessageScheduler();

        y.Host("localhost","/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        y.ConfigureEndpoints(x);
    });
});

//Add Database for saga persistence!

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
