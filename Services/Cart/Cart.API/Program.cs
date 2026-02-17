using BuildingBlocks.Messaging.MassTransit;
using Cart.API.Services;
using Cart.Application.Handlers.Commands;
using Cart.Application.Handlers.Events;
using Cart.Application.Handlers.Queries;
using Cart.Application.Interfaces;
using Cart.Domain.Interfaces;
using Cart.Infrastructure.Repositories;
using Catalog.gRPC;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Session;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static DiscountService.gRPC.DiscountService;


var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICatalogGrpcService, CatalogGrpcServiceClient>();
builder.Services.AddScoped<IDiscountGrpcService, DiscountGrpcServiceClient>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<CurrentSession>();

// JWT Authentication
var jwtKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
        };
    });
builder.Services.AddAuthorization();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

app.Run();

