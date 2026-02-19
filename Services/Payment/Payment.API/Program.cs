using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Payment.API.DTOs;
using Stripe;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly!);
    //  config.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly);
    // config.RegisterServicesFromAssembly(typeof(CancelOrderHandler).Assembly);
});

builder.Services.AddCarter();

builder.Services.AddMessageBroker(builder.Configuration, [assembly]);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");

// STRIPE CONFIG
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

app.MapPost("/api/create-payment-intent", async (PaymentIntentDto request) =>
{
    var metadata = new Dictionary<string, string>();
    metadata.Add("OrderId", request.OrderId.ToString());
    metadata.Add("CustomerId", request.CustomerId.ToString());
    metadata.Add("Amount", request.Amount.ToString());

    var options = new PaymentIntentCreateOptions
    {
        Metadata = metadata,
        Amount = (long)request.Amount,
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

app.MapCarter();

app.Run();
