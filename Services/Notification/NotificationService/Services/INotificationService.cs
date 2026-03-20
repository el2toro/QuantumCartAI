namespace Notification.Application.Services;

public interface INotificationService
{
    Task SendEmail(string toEmail, string subject, string htmlMessage);
    Task SendSms(string toNumber, string message);
    Task SendPushNotification(string email);
}
