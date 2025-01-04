var builder = WebApplication.CreateBuilder(args);


#region DI Container
builder.Services.AddMediatR(cfg => 
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
    {
        opt.Connection(builder.Configuration.GetConnectionString("LocalDb")!);
    })
    .UseLightweightSessions();
#endregion

var app = builder.Build();

app.MapCarter();

app.Run();
