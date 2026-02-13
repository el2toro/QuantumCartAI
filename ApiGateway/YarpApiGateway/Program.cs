using Microsoft.AspNetCore.RateLimiting;
using QuantumCartAI.Shared.Infrastructure.AspNetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddECommerceAspNetInfrastructure();
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        //In an interval of 10 second can be sended 5 requasts as limit
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

const string corsPolicy = "luxe-ecommerce-policy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    if (!context.Request.Cookies.TryGetValue("anonymous-id", out var anonymousId))
    {
        anonymousId = Guid.NewGuid().ToString();
        context.Response.Cookies.Append("anonymous-id", anonymousId,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = context.Request.IsHttps,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(90)
            });
    }

    context.Request.Headers["X-Anonymous-Id"] = anonymousId;

    await next();
});


app.MapGet("/", () => "Yarp Gateway is Healthy!");

app.UseRateLimiter();
app.MapReverseProxy();
app.UseCors(corsPolicy);

app.Run();

