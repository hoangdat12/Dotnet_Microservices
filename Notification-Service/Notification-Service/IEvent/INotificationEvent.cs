

namespace Notification_Service.IEvent
{
    public interface INotificationEvent: IIntergateEvent
    {
        public string Name {get; set;} // email or phone number
        public string UserName {get; set;}
        public string Type {get; set;} // email or sms
        public string Content {get; set;} 
        public string Title { get; set;}
    }
}