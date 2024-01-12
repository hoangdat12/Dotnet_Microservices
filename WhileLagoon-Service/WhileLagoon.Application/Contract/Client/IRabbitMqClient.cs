
namespace WhileLagoon.Application.Contract.Client
{
    public interface IRabbitMqClient
    {
        public void PublishMessage<T>(string exchangeName, string routingKey, T message);

        public void CloseConnection();
    }
}