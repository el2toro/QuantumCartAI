using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.GetProducts;
using Catalog.API.Repositories;
using Catalog.API.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Shared infrastructure (CurrentSession + IHttpContextAccessor)
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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.MapCarter();

// Map the health check endpoint
app.MapHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.MapGrpcService<CatalogGrpcService>();

app.Run();

