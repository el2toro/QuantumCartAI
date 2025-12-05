using Payment.API.DTOs;
using Stripe;
using Stripe.Checkout;

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
    var lineItems = new List<SessionLineItemOptions>();
    long totalAmount = 0;
    string currency = string.Empty;

    foreach (var item in request.Items)
    {
        lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = item.PriceData.Currency,
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.PriceData.ProductData.Name,
                    Images = item.PriceData.ProductData.Images
                },
                UnitAmount = item.PriceData.UnitAmount,
            },
            Quantity = item.Quantity,
        });

        totalAmount += item.PriceData.UnitAmount * item.Quantity;
        currency = item.PriceData.Currency;
    }

    var options = new PaymentIntentCreateOptions
    {
        Amount = totalAmount,
        Currency = currency,
        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
        {
            Enabled = true           // <-- enables ALL eligible payment methods
        }
    };

    var service = new PaymentIntentService();
    var paymentIntent = await service.CreateAsync(options);

    return Results.Ok(new { clientSecret = paymentIntent.ClientSecret });
});

app.MapPost("/webhook", async (HttpRequest req) =>
{
    var json = await new StreamReader(req.Body).ReadToEndAsync();
    var stripeEvent = EventUtility.ConstructEvent(
        json,
        req.Headers["Stripe-Signature"],
        builder.Configuration["Stripe:WebhookSecret"]
    );

    if (stripeEvent.Type == "payment_intent.succeeded")
    {
        var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
        Console.WriteLine($"$3.3M PAID — CLIENT: {paymentIntent?.Metadata["client_tier"]}");
        // Send SMS, dispatch jet, mint NFT, etc.
    }

    return Results.Ok();
});

app.Run();
