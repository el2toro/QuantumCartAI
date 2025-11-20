using BuildingBlocks.Messaging.MassTransit;
using Cart.API.Services;
using Cart.Application.EventHandlers;
using Cart.Application.Handlers;
using Cart.Application.Interfaces;
using Catalog.gRPC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddScoped<ICatalogGrpcService, CatalogGrpcServiceClient>();
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddItemHandler).Assembly);
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "CartService";
});

var eventHandlerAssemblies = new Assembly[]
{
    typeof(CartItemAddedEventHandler).Assembly,
   // typeof(ProjectUpdatedEventHandler).Assembly,
   // typeof(ProjectDeletedEventHandler).Assembly
};

builder.Services.AddMessageBroker(builder.Configuration, eventHandlerAssemblies);

//gRPC Services
builder.Services.AddGrpcClient<CatalogQueryService.CatalogQueryServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:CatalogUrl"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.MapGet("grpc", () => "OK");
app.Run();

