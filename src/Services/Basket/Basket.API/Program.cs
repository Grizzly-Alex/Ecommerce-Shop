using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
var dbConnectionString = builder.Configuration.GetConnectionString("Database")!;


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

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(setup =>
{
    setup.Configuration = builder.Configuration.GetConnectionString("Cacher");
    setup.InstanceName = "Basket";
});

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Cacher")!);
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHealthChecks("/health", 
    new HealthCheckOptions 
    {   
         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse         
    });

app.MapCarter();

app.Run();
