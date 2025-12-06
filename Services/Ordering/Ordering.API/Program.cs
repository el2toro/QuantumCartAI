using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Handlers.Commands;
using Ordering.Application.Interfaces;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IOrderingRepository, OrderingRepository>();
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    //config.RegisterServicesFromAssembly(assembly!);
    config.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(CancelOrderHandler).Assembly);
});

builder.Services.AddDbContext<OrderingDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddMessageBroker(builder.Configuration, [typeof(CreateOrderHandler).Assembly]);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
