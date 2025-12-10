using Carter;
using Chat.API.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();

