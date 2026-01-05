using BuildingBlockks.Behaviors;
using BuildingBlockks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

//Add Service
var Assembly = typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly);
    config.AddOpenBehavior(typeof(ValidationBehviors<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database"));
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BaskerRepository>();
builder.Services.Decorate<IBasketRepository , CachedBasketRepository>();


builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();

//Add Http Pipeline

app.MapGet("/", () => "Hello World!");
app.UseExceptionHandler(options => { });
app.MapCarter();

app.Run();
