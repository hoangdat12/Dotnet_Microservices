
using Notification_Service.IEvent;

namespace Notification_Service.Event
{
    public class IntergrateEvent: IIntergateEvent
    {
        public Guid Id {get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}