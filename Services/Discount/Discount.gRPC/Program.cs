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
    config.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
        db.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountServiceImplementation>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client");

app.Run();
