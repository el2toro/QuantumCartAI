using Carter;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetAssembly(typeof(Program));
// Add services to the container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly!);
});

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.MapGet("/", () =>
{
    return "This is working!";
});

app.Run();

