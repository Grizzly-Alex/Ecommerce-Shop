var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


#region DI Container
var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(cfg => 
    {
        cfg.RegisterServicesFromAssembly(assembly);
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        cfg.AddOpenBehavior(typeof(LoggingBenavior<,>));
    });

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
    {
        opt.Connection(builder.Configuration.GetConnectionString("LocalDb")!);
    }).UseLightweightSessions();

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddProblemDetails();
#endregion

var app = builder.Build();

app.UseExceptionHandler();

app.MapCarter();


app.Run();
