using CatalogAPI.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to container
var assembly = typeof(Program).Assembly;
// Register Carter Library, Marten DB
builder.Services.AddCarter();
builder.Services.AddMediatR(conf =>
    {
        conf.RegisterServicesFromAssembly(assembly);
        conf.AddOpenBehavior(typeof(ValidationBehaviors<,>));
        conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
    });

builder.Services.AddValidatorsFromAssembly(assembly)
    .AddMarten(opts => 
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    })
    .UseLightweightSessions();

if(builder.Environment.IsDevelopment()) 
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
        .AddNpgSql(builder.Configuration.GetConnectionString("Database")!); 

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

// Required for CustomExceptionHandler
app.UseExceptionHandler(opt => { });

app.UseHealthChecks("/healthz",
    new HealthCheckOptions
    { 
        ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse
    }
);

// Build
app.Run();
