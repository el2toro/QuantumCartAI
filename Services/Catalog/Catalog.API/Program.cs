using Carter;
using Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetAssembly(typeof(Program));
// Add services to the container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly!);
});

builder.Services.AddCarter();

builder.Services.AddDbContext<ProductDbContext>(config =>
{
    config.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddNpgSql(
      builder.Configuration.GetConnectionString("Database")!,
      name: "PostgreSQL",
      failureStatus: HealthStatus.Degraded);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

// Map the health check endpoint
app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();

