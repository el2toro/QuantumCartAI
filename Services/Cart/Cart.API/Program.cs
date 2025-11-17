using Cart.Application.Adapters;
using Cart.Application.Handlers;
using Cart.Application.Queries;
using Cart.Domain.Interfaces;
using Cart.Domain.Ports;
using Cart.Infrastructure.Repositories;
using EventStore.Client;
using MediatR;
using QuantumCartAI.Service.Cart.proto;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddScoped<ICartQueryService, CartQueryService>();
builder.Services.AddScoped<ICartRepository, EventStoreCartRepository>();
builder.Services.AddScoped<IInventoryQuery, InventoryQueryAdapter>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddItemHandler).Assembly);
});

builder.Services.AddGrpcClient<InventoryQueryService.InventoryQueryServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Inventory:GrpcUrl"]
        ?? "https://localhost:5001");   // fallback for local dev
});

builder.Services.AddHttpClient("Inventory", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Inventory:HttpUrl"]);
});

builder.Services.AddSingleton<EventStoreClient>(sp =>
    new EventStoreClient(EventStoreClientSettings.Create(
        builder.Configuration["EventStore:ConnectionString"])));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapPost("/carts/{cartId}/items", async (AddItemRequest request, IMediator mediator) =>
    Results.Ok(await mediator.Send(new AddItemCommand(request.CartId, request.SkuId, request.Quantity))));

app.MapGet("/carts/{cartId}", async (Guid cartId, ICartQueryService query) =>
    Results.Ok(await query.GetCartAsync(cartId)));

app.Run();

public record AddItemRequest(Guid CartId, string SkuId, int Quantity);
