

using ProductService.Application.Event;

namespace ProductService.Infrustructure.Service.RabbitMq.Event
{
    public class NotificationEvent : INotificationEvent
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string Name {get; set;}
        public string Type {get; set;}
        public string Content {get; set;}
        public string Title { get; set;}
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}