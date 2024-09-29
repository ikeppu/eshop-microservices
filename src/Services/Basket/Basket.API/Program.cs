using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
// Add services to the container
builder.Services.AddCarter();

builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(assembly);
    conf.AddOpenBehavior(typeof(ValidationBehaviors<,>));
    conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
