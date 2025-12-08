using BuildingBlocks.Messaging.Events;
using BuildingBlocks.Messaging.MassTransit;
using MassTransit;
using Payment.API.DTOs;
using Stripe;
using Stripe.Checkout;
using System.Reflection;
using System.Text;

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

var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly!);
    //  config.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly);
    // config.RegisterServicesFromAssembly(typeof(CancelOrderHandler).Assembly);
});

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

app.MapPost("/webhook", async (HttpRequest request, IPublishEndpoint publishEndpoint) =>
{
    var json = await new StreamReader(request.Body).ReadToEndAsync();
    var stripeSignature = request.Headers["Stripe-Signature"];
    var webhookSecret = builder.Configuration["Stripe:WebhookSecret"];
    try
    {

        var stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, webhookSecret);

        if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            Console.WriteLine($"$3.3M PAID — CLIENT: {paymentIntent?.Metadata}");
            // Send SMS, dispatch jet, mint NFT, etc.
            //await _paymentService.MarkOrderAsPaid(intent.Id);

            Guid orderId = Guid.Parse(paymentIntent?.Metadata["OrderId"]!);
            Guid customerId = Guid.Parse(paymentIntent?.Metadata["CustomerId"]!);
            decimal.TryParse(UTF8Encoding.UTF8.GetBytes(paymentIntent?.Metadata["Amount"]!), out decimal amount);
            string paymentMethod = paymentIntent?.PaymentMethod?.Type ?? "card";

            await publishEndpoint.Publish<PaymentSucceededEvent>(new(orderId, customerId, amount, paymentMethod));
        }
    }
    catch (Exception ex)
    {

        throw;
    }
    //else if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
    //{
    //    var intent = stripeEvent.Data.Object as PaymentIntent;

    //    await _paymentService.MarkOrderAsFailed(intent.Id);
    //}

    return Results.Ok();
});

app.Run();
