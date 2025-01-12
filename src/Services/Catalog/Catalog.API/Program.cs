using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
var dbConnectionString = builder.Configuration.GetConnectionString("Database")!;


builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


#region DI Container
builder.Services
    .AddExceptionHandler<CustomExceptionHandler>()
    .AddProblemDetails();

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
        opt.Connection(dbConnectionString);
    }).UseLightweightSessions();

builder.Services.AddHealthChecks()
    .AddNpgSql(dbConnectionString);
#endregion

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
};

var app = builder.Build();

app.UseExceptionHandler();

app.MapCarter();

app.UseHealthChecks("/health",
    new HealthCheckOptions()
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
