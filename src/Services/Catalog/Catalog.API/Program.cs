var builder = WebApplication.CreateBuilder(args);

#region DI Container
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
#endregion

var app = builder.Build();

app.MapCarter();

app.Run();
