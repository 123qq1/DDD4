var builder = WebApplication.CreateBuilder(args);

var startup = new StartupBase(builder.Configuration);

var app = builder.Build();



app.Run();
