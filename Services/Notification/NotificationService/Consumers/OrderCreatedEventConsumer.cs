using BuildingBlocks.Messaging.IntegrationEvents.Order;
using MassTransit;
using Notification.Application.Services;

namespace Notification.Application.Consumers;

public class OrderCreatedEventConsumer(INotificationService notificationService) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        string toEmail = "application.service001@gmail.com";
        string subject = $"Order Confirmation – Order #{context.Message.OrderNumber}";

        string htmlMessage = "<!DOCTYPE html>\r\n\r\n<html>\r\n<head>\r\n<meta charset=\"UTF-8\">\r\n<title>Order Confirmation</title>\r\n<style>\r\n  body {\r\n    font-family: Arial, Helvetica, sans-serif;\r\n    background-color: #f4f6f8;\r\n    margin: 0;\r\n    padding: 0;\r\n  }\r\n  .container {\r\n    max-width: 640px;\r\n    margin: 40px auto;\r\n    background: #ffffff;\r\n    border-radius: 8px;\r\n    overflow: hidden;\r\n    box-shadow: 0 2px 8px rgba(0,0,0,0.05);\r\n  }\r\n  .header {\r\n    background: #1f2937;\r\n    color: #ffffff;\r\n    padding: 20px 30px;\r\n    font-size: 20px;\r\n    font-weight: bold;\r\n  }\r\n  .content {\r\n    padding: 30px;\r\n    color: #333333;\r\n    line-height: 1.6;\r\n  }\r\n  .order-box {\r\n    background: #f9fafb;\r\n    border: 1px solid #e5e7eb;\r\n    border-radius: 6px;\r\n    padding: 20px;\r\n    margin-top: 20px;\r\n  }\r\n  table {\r\n    width: 100%;\r\n    border-collapse: collapse;\r\n    margin-top: 10px;\r\n  }\r\n  th {\r\n    text-align: left;\r\n    background: #f3f4f6;\r\n    padding: 10px;\r\n    font-size: 13px;\r\n  }\r\n  td {\r\n    padding: 10px;\r\n    border-bottom: 1px solid #e5e7eb;\r\n    font-size: 14px;\r\n  }\r\n  .total {\r\n    text-align: right;\r\n    font-weight: bold;\r\n    font-size: 16px;\r\n    padding-top: 15px;\r\n  }\r\n  .footer {\r\n    background: #f9fafb;\r\n    text-align: center;\r\n    font-size: 12px;\r\n    color: #6b7280;\r\n    padding: 20px;\r\n  }\r\n</style>\r\n</head>\r\n\r\n<body>\r\n\r\n<div class=\"container\">\r\n\r\n  <div class=\"header\">\r\n    NovaTech Store\r\n  </div>\r\n\r\n  <div class=\"content\">\r\n    <p>Hello <strong>Michael Anderson</strong>,</p>\r\n\r\n```\r\n<p>Thank you for your purchase! Your order has been successfully created and is currently being processed.</p>\r\n\r\n<div class=\"order-box\">\r\n  <strong>Order Information</strong>\r\n\r\n  <table>\r\n    <tr>\r\n      <th>Order Number</th>\r\n      <td>#104582</td>\r\n    </tr>\r\n    <tr>\r\n      <th>Order Date</th>\r\n      <td>March 16, 2026</td>\r\n    </tr>\r\n    <tr>\r\n      <th>Shipping Address</th>\r\n      <td>\r\n        Michael Anderson<br>\r\n        214 Maple Street<br>\r\n        Chicago, IL 60610<br>\r\n        United States\r\n      </td>\r\n    </tr>\r\n  </table>\r\n\r\n  <table>\r\n    <tr>\r\n      <th>Product</th>\r\n      <th>Qty</th>\r\n      <th>Price</th>\r\n    </tr>\r\n\r\n    <tr>\r\n      <td>Wireless Bluetooth Headphones</td>\r\n      <td>1</td>\r\n      <td>$129.00</td>\r\n    </tr>\r\n\r\n    <tr>\r\n      <td>USB-C Fast Charging Cable</td>\r\n      <td>2</td>\r\n      <td>$18.00</td>\r\n    </tr>\r\n  </table>\r\n\r\n  <div class=\"total\">\r\n    Total: $165.00\r\n  </div>\r\n</div>\r\n\r\n<p style=\"margin-top:25px;\">\r\n  We will notify you once your order has been shipped.  \r\n  If you have any questions, feel free to contact our support team.\r\n</p>\r\n\r\n<p>\r\n  Best regards,<br>\r\n  <strong>NovaTech Store Team</strong><br>\r\n  support@novatechstore.com\r\n</p>\r\n```\r\n\r\n  </div>\r\n\r\n  <div class=\"footer\">\r\n    © 2026 NovaTech Store — All rights reserved<br>\r\n    120 Market Street, San Francisco, CA\r\n  </div>\r\n\r\n</div>\r\n\r\n</body>\r\n</html>\r\n";

        try
        {
            await notificationService.SendEmail(toEmail, subject, htmlMessage);
            await notificationService.SendSms("076775951", "02365156");
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
