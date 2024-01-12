
namespace ProductService.Application.Contract.RabbitMq
{
    public interface IRabbitMqClient
    {
        public void PublishMessage<T>(string exchangeName, string routingKey, T message);
        public void CloseConnection();
    }
}