using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
var dbConnectionString = builder.Configuration.GetConnectionString("Database")!;


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBenavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Basket API",
        Description = "ASP.NET Core 8 Web API for managing shopping cart data, supporting CRUD operations and health check",
        Contact = new OpenApiContact
        {
            Name = "Alexander Medved",
            Url = new Uri("https://github.com/Grizzly-Alex")
        }
    });
});

builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
{
    opt.Connection(dbConnectionString);
    opt.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
    opt.Schema.For<ShoppingCart>().Identity(prop => prop.UserId);
}).UseLightweightSessions();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();

app.Run();
