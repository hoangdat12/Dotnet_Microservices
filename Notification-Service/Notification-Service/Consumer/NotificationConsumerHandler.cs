
using System.Text.Json;
using Notification_Service.Event;
using Notification_Service.Service;

namespace Notification_Service.Consumer
{
    public class NotificationConsumerHandler(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task Handler(string message) 
        {
            Console.WriteLine(message);

            NotificationEvent decodeMsg = JsonSerializer.Deserialize<NotificationEvent>(message);

            var mailService = new MailService(_configuration);
            await mailService.SendMailAsync(decodeMsg);
        }
    }
}