using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Client;
using WhileLagoon.Application.Model;

namespace WhileLagoon.Infrastructure.Service.rabbitMq
{
    public class RabbitMqClient: IRabbitMqClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitMqClient(IConfiguration configuration)
        {
            _configuration = configuration;
            RabbitMQConfiguration rabbitMQConfiguration = new();
            _configuration.GetSection(AppSetting.RabbitMQConfiguration).Bind(rabbitMQConfiguration);

            var factory = new ConnectionFactory
            {
                HostName = rabbitMQConfiguration.Host, 
                UserName = rabbitMQConfiguration.Username,
                Password = rabbitMQConfiguration.Password
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void PublishMessage<T>(string exchangeName, string routingKey, T message)
        {
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body: body);

            Console.WriteLine($"Sent message to exchange '{exchangeName}' with routing key '{routingKey}': {message}");
        
            CloseConnection();
        }

        public void CloseConnection()
        {
            channel.Close();
            connection.Close();
        }
    }
}