var builder = WebApplication.CreateBuilder(args);


#region DI Container
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg => 
    {
        cfg.RegisterServicesFromAssembly(assembly);
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    });

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
    {
        opt.Connection(builder.Configuration.GetConnectionString("LocalDb")!);
    }).UseLightweightSessions();
#endregion

var app = builder.Build();

app.MapCarter();

app.Run();
