using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Handlers.Commands;
using Ordering.Application.Handlers.Events;
using Ordering.Application.Interfaces;
using Ordering.Application.Mappings;
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
    config.RegisterServicesFromAssembly(typeof(PaymentSucceededEventHandler).Assembly);
});

builder.Services.AddDbContext<OrderingDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

const string corsPolicy = "luxe-ecommerce-policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()     // needed if sending cookies or Authorization headers
            .SetIsOriginAllowed(origin => true); // allow all origins (unsafe for prod, adjust below)
    });
});

builder.Services.AddMessageBroker(builder.Configuration, [typeof(CreateOrderHandler).Assembly]);

MapsterConfig.RegisterMappings();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(corsPolicy);

app.MapCarter();

app.Run();
