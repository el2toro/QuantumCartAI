using Payment.API.DTOs;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7163, listenOptions =>
    {
        var pathToCertificate = builder.Configuration["Https:Certificate:Path"]!;
        var certificatePassword = builder.Configuration["Https:Certificate:Password"];
        listenOptions.UseHttps(pathToCertificate, certificatePassword);
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");

// STRIPE CONFIG
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

app.MapPost("/api/create-payment-intent", async (PaymentIntentDto request) =>
{
    var options = new PaymentIntentCreateOptions
    {
        Amount = request.Amount,
        Currency = request.Currency,
        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
        {
            Enabled = true           // <-- enables ALL eligible payment methods
        }
    };

    var service = new PaymentIntentService();
    var paymentIntent = await service.CreateAsync(options);

    return Results.Ok(new { clientSecret = paymentIntent.ClientSecret });
});

app.Run();
