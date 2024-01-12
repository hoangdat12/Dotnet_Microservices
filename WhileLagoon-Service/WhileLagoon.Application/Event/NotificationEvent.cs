
namespace WhileLagoon.Application.Event
{
    public class NotificationEvent: IntergrateEvent, INotificationEvent
    {
        public string Type {get; set;}
        public string UserName {get; set;}
        public string Title {get; set;}
        public string Name {get; set;}
        public string Content {get; set;} 
    }
}