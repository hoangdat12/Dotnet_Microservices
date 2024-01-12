using MailKit.Net.Smtp;
using MimeKit;
using Notification_Service.Constant;
using Notification_Service.Event;
using Notification_Service.IEvent;
using Notification_Service.Model;
using Notification_Service.Template;
using NotificationService.Application.Constant;

namespace Notification_Service.Service
{
    public class MailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailConfiguration _mailConfiguration;
        
        public MailService(IConfiguration configuration) 
        {
            _configuration = configuration;

            MailConfiguration mailConfiguration = new();
            _configuration.GetSection(AppSetting.MailConfiguration).Bind(mailConfiguration);

            _mailConfiguration = mailConfiguration;
        }

        public async Task SendMailAsync(NotificationEvent notificationEvent) 
        {
            MimeMessage email = new();

            email.Sender = new MailboxAddress(
                _mailConfiguration.DisplayName,
                _mailConfiguration.Gmail
            );
            email.From.Add(new MailboxAddress(
                _mailConfiguration.DisplayName,
                _mailConfiguration.Gmail
            ));

            email.To.Add(new MailboxAddress(
                notificationEvent.Name,
                notificationEvent.Name
            ));

            BodyBuilder body = new();
            // body.HtmlBody = 
            body.TextBody = GetMessageBody(notificationEvent);

            email.Body = body.ToMessageBody();
            email.Subject = notificationEvent.Title;

            using var smtp = new SmtpClient();
            try {
                await smtp.ConnectAsync(
                    _mailConfiguration.Host, 
                    _mailConfiguration.Port, 
                    MailKit.Security.SecureSocketOptions.StartTls
                );
                await smtp.AuthenticateAsync(_mailConfiguration.Gmail, _mailConfiguration.Password);
                await smtp.SendAsync(email);
            } catch(Exception e) 
            {
                Console.WriteLine(e.ToString());
            }

            smtp.Disconnect(true);
        }

        private string GetMessageBody(INotificationEvent notificationEvent)
        {

            return notificationEvent.Title switch
            {
                EmailType.ActiveAccount => EmailTemplate.ActiveAccount(
                                        notificationEvent.UserName,
                                        notificationEvent.Content
                                    ),
                EmailType.ForgotPassword => EmailTemplate.ForgotPassword(
                                        notificationEvent.UserName,
                                        notificationEvent.Content
                                    ),
                _ => throw new Exception("Type not found!"),
            };
        }
    }
}