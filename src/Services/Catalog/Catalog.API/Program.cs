using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);


#region DI Container
builder.Services.AddMediatR(cfg => 
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
    {
        opt.Connection(builder.Configuration.GetConnectionString("LocalDb")!);
    }).UseLightweightSessions();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
#endregion

var app = builder.Build();

app.MapCarter();

app.Run();
