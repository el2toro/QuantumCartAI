using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Ordering.Application.Handlers.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    //config.RegisterServicesFromAssembly(assembly!);
    config.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(CancelOrderHandler).Assembly);
});


builder.Services.AddMessageBroker(builder.Configuration, [typeof(CreateOrderHandler).Assembly]);

var app = builder.Build();
app.MapCarter();
// Configure the HTTP request pipeline.

app.Run();
