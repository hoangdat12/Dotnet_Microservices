using System.Text;
using Notification_Service.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification_Service.Client
{
    public class RabbitMqClient
    {
        private readonly RabbitMQConfiguration _configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

        public RabbitMqClient(RabbitMQConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory
            {
                HostName = configuration.Host, // RabbitMQ server address
                UserName = configuration.Username,
                Password = configuration.Password
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void SetupQueue(string queueName, string routingKey, string exchangeType)
        {
            // Declare exchange
            channel.ExchangeDeclare(
                _configuration.NotificationExchangeName, 
                exchangeType
            );

            // Declare queue
            channel.QueueDeclare(
                queueName, 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null
            );

            // Bind queue to exchange
            channel.QueueBind(
                queueName, 
                _configuration.NotificationExchangeName, 
                routingKey
            );
        }

        public void SetupDeadLetterExchange(
            string queueName, 
            string dlxQueueName,
            string routingKey, 
            string exchangeType
        ) 
        {
            // Declare exchange
            channel.ExchangeDeclare(
                _configuration.NotificationExchangeName, 
                exchangeType
            );
            channel.ExchangeDeclare(
                _configuration.DlxNotificationExchangeName, 
                ExchangeType.Fanout
            );

            var arguments = new Dictionary<string, object>()
            {
                {"x-dead-letter-exchange", _configuration.DlxNotificationExchangeName},
                {"x-message-ttl", 1000}
            };

            // Declare queue
            channel.QueueDeclare(
                queueName, 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: arguments
            );
            channel.QueueDeclare(
                dlxQueueName, 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null
            );

            // Bind queue to exchange
            channel.QueueBind(
                queueName, 
                _configuration.NotificationExchangeName, 
                routingKey
            );
            channel.QueueBind(
                dlxQueueName, 
                _configuration.DlxNotificationExchangeName, 
                ""
            );
        }

        public void Consume(string queueName, Action<string> handleMessage)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray(); 
                var message = Encoding.UTF8.GetString(body);

                // Process the message
                handleMessage(message);

                // Acknowledge the message
                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        }

        public void CloseConnection()
        {
            channel.Close();
            connection.Close();
        }
    }
}