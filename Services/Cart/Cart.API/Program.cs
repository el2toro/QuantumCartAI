using BuildingBlocks.Messaging.MassTransit;
using Cart.API.Middleware;
using Cart.API.Services;
using Cart.API.Session;
using Cart.Application.Handlers.Commands;
using Cart.Application.Handlers.Events;
using Cart.Application.Handlers.Queries;
using Cart.Application.Interfaces;
using Cart.Domain.Interfaces;
using Cart.Infrastructure.Repositories;
using Catalog.gRPC;
using System.Reflection;
using static DiscountService.gRPC.DiscountService;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CurrentSession>();

builder.Services.AddScoped<ICatalogGrpcService, CatalogGrpcServiceClient>();
builder.Services.AddScoped<IDiscountGrpcService, DiscountGrpcServiceClient>();
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(AddItemHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CartCheckoutHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCartQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CartItemAddedEventHandler).Assembly);
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

builder.Services.AddGrpcClient<DiscountServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
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

const string corsPolicy = "luxe-eccomerce-policy";
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

var app = builder.Build();

app.UseAnonymousSession();
app.UseCors(corsPolicy);
// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();

