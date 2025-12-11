using Discount.gRPC.Data;
using Discount.gRPC.Repositories;
using Discount.gRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddDbContext<DiscountDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client");

app.Run();
