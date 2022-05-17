using System.Text;
using System.Text.Json;
using Logging.Consumer;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = builder.Build();

const string QUEUE_NAME = "messagelogs";
var factory = new ConnectionFactory { HostName = configuration["RabbitMqUrl"] };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: QUEUE_NAME,
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

    channel.BasicConsume(queue: QUEUE_NAME,
        autoAck: true,
        consumer: consumer);

    Thread.Sleep(2000);
}
