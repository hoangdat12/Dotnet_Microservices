
namespace WhileLagoon.Application.Event
{
    public interface IIntergateEvent
    {
        public Guid Id {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}