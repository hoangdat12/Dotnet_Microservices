

namespace Notification_Service.Model
{
    public class RabbitMQConfiguration
    {
        public string Host {get; set;}
        public string VHost {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string NotificationExchangeName {get; set;}
        public string DlxNotificationExchangeName {get; set;}
        public string NotificationExchangeType {get; set;}
        public string NotifyQueueName {get; set;}
        public string DLXNotifyQueueName {get; set;}
        public int RetryLimit {get; set;}
        public int InitialInterval {get; set;}
        public int IntervalIncrement {get; set;}
    }
}