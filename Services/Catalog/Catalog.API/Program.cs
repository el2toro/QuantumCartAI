using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using Catalog.API.Interfaces;
using Catalog.API.Repositories;
using Catalog.API.Services;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProducts;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetAssembly(typeof(Program));
// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddMediatR(config =>
{
    //config.RegisterServicesFromAssembly(assembly!);
    config.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly);
    config.RegisterServicesFromAssembly(typeof(GetProductsHandler).Assembly);
});

builder.Services.AddCarter();

builder.Services.AddDbContext<ProductDbContext>(config =>
{
    config.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddNpgSql(
      builder.Configuration.GetConnectionString("Database")!,
      name: "PostgreSQL CatalogDb",
      failureStatus: HealthStatus.Degraded);

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
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

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseCors(corsPolicy);

// Map the health check endpoint
app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.MapGrpcService<CatalogGrpcService>();
app.MapGet("grpc", () => "OK");
app.Run();

