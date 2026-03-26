using Microsoft.Extensions.Options;
using Notification.Application.Configurations;
using SendGrid;
using SendGrid.Helpers.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Notification.Application.Services;

public class NotificationService : INotificationService
{
    private readonly SendGridOptions _sendGridOptions;
    private readonly TwilioOptions _twilioOptions;

    public NotificationService(IOptions<SendGridOptions> sendGridOptions, IOptions<TwilioOptions> twilioOptions)
    {
        _sendGridOptions = sendGridOptions.Value;
        _twilioOptions = twilioOptions.Value;

        TwilioClient.Init(_twilioOptions.AccountSid, _twilioOptions.AuthToken);
    }
    public async Task SendEmail(string toEmail, string subject, string htmlMessage)
    {
        var client = new SendGridClient(_sendGridOptions.ApiKey);

        var from = new EmailAddress(_sendGridOptions.FromEmail, _sendGridOptions.FromName);
        var to = new EmailAddress(toEmail);

        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            plainTextContent: htmlMessage,
            htmlContent: htmlMessage);

        await client.SendEmailAsync(msg);
    }

    public Task SendPushNotification(string email)
    {
        throw new NotImplementedException();
    }

    public async Task SendSms(string toNumber, string message)
    {
        var msg = await MessageResource.CreateAsync(
            to: new PhoneNumber(toNumber),
            from: new PhoneNumber(_twilioOptions.FromNumber),
            body: message
        );
    }
}
