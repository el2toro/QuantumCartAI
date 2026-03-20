using BuildingBlocks.Messaging.MassTransit;
using Notification.Application.Configurations;
using Notification.Application.Consumers;
using Notification.Application.Services;
using Notification.Worker;
using SendGrid;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddMessageBroker(builder.Configuration, [typeof(OrderCreatedEventConsumer).Assembly]);
builder.Services.Configure<SendGridClientOptions>(
    builder.Configuration.GetSection("SendGrid"));

builder.Services.Configure<TwilioOptions>(
    builder.Configuration.GetSection("Twilio"));

var host = builder.Build();

host.Run();
