using Logging.Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

const string queueName = "messagelogs";
var factory = new ConnectionFactory { HostName = new DataBaseConfiguration().GetRabbitMqUrl() };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

while (true)
{
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += async (_, basicDeliverEventArgs) =>
                {
                    var body = basicDeliverEventArgs.Body.ToArray();
                    var returnMessage = Encoding.UTF8.GetString(body);
                    var message = JsonSerializer.Deserialize<LogRequest>(returnMessage);
                    await LogClient.Add(message!);
                };

    channel.BasicConsume(queue: queueName,
        autoAck: true,
        consumer: consumer);

    Thread.Sleep(2000);
}
