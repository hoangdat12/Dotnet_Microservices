using MailKit;
using Notification_Service.Client;
using Notification_Service.Consumer;
using Notification_Service.Model;
using NotificationService.Application.Constant;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

RabbitMQConfiguration rabbitMQConfiguration = new();
builder.Configuration.GetSection(AppSetting.RabbitMQConfiguration).Bind(rabbitMQConfiguration);

var rabbitMQClient = new RabbitMqClient(rabbitMQConfiguration);
    rabbitMQClient.SetupDeadLetterExchange(
        rabbitMQConfiguration.NotifyQueueName, 
        rabbitMQConfiguration.DLXNotifyQueueName, 
        "notify", 
        ExchangeType.Topic
    );

    rabbitMQClient.Consume(rabbitMQConfiguration.NotifyQueueName, async message =>
    {
        NotificationConsumerHandler notificationConsumerHandler = new(builder.Configuration);
        await notificationConsumerHandler.Handler(message);
    });
    rabbitMQClient.Consume(rabbitMQConfiguration.DLXNotifyQueueName, message =>
    {
        // SmsConsumerHandler smsConsumerHandler = new();
        // smsConsumerHandler.Handler<INotificationEvent>(message);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
