
namespace ProductService.Application.Event
{
    public interface INotificationEvent
    {
        public Guid Id {get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
        public string Content {get; set;}
        public string Title { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}