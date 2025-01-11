using Catalog.API.Data;

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
        opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    }).UseLightweightSessions();

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddProblemDetails();
#endregion

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
};

var app = builder.Build();

app.UseExceptionHandler();

app.MapCarter();


app.Run();
